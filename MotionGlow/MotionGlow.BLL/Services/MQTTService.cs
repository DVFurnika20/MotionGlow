using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MotionGlow.BLL.IServices;
using MotionGlow.DAL.Data;
using MotionGlow.DAL.Models;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MotionGlow.BLL.Services
{
    public class MqttService : IMqttService
    {
        private readonly IPIRSensorService _pirSensorService;
        private readonly ISoundSensorService _soundSensorService;
        private readonly IESP32_DeviceService _esp32DeviceService;
        private readonly ISensorActivityLogService _sensorActivityLogService;
        private readonly IMqttClient _mqttClient;
        private readonly List<SensorData> _sensorDataList;
        private readonly IServiceScopeFactory _scopeFactory;

        public MqttService(
            IPIRSensorService pirSensorService,
            ISoundSensorService soundSensorService,
            IESP32_DeviceService esp32DeviceService,
            ISensorActivityLogService sensorActivityLogService,
            IConfiguration configuration,
            IServiceScopeFactory scopeFactory)
        {
            _pirSensorService = pirSensorService;
            _soundSensorService = soundSensorService;
            _esp32DeviceService = esp32DeviceService;
            _sensorActivityLogService = sensorActivityLogService;

            // Create a new MQTT client
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            _sensorDataList = new List<SensorData>();
            _scopeFactory = scopeFactory;

            var brokerUrl = configuration["MQTT:BrokerUrl"];
            var clientId = configuration["MQTT:ClientId"];
            var username = configuration["MQTT:Username"];
            var password = configuration["MQTT:Password"];
        }

        public async Task StartAsync(string brokerUrl, string clientId, string username, string password)
        {
            // Configure MQTT client options
            var options = new MqttClientOptionsBuilder()
                .WithWebSocketServer(brokerUrl)
                .WithTls()
                .WithClientId(clientId)
                .WithCredentials(username, password)
                .Build();

            // Connect to MQTT broker
            await _mqttClient.ConnectAsync(options);

            // Subscribe to MQTT topics
            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                // Process the received message
                ProcessReceivedMessage(e.ApplicationMessage);
            });

            // After accumulating data, process all of it

            await _mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("+/pir_sensor").Build());
            await _mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("+/sound_sensor").Build());
        }

        public async Task DisconnectAsync()
        {
            await _mqttClient.DisconnectAsync();
        }

        private async void ProcessReceivedMessage(MqttApplicationMessage message)
        {
            Console.WriteLine("received message");


            string topic = message.Topic;
            byte[] payload = message.Payload;
            int clientId = Convert.ToInt32(topic.Substring(0, topic.IndexOf('/')));

            if (topic.EndsWith("pir_sensor"))
            {
                bool pirValue = (Convert.ToInt32(Encoding.UTF8.GetString(payload)) == 1);
                _sensorDataList.Add(new SensorData { PIRSensorValue = pirValue, Timestamp = DateTime.Now, ClientId = clientId});

                Console.WriteLine("received pir: " + pirValue);
            }
            else if (topic.EndsWith("sound_sensor"))
            {
                int soundLevel = Convert.ToInt32(Encoding.UTF8.GetString(payload));
                _sensorDataList.Add(new SensorData { SoundSensorLevel = soundLevel, Timestamp = DateTime.Now, ClientId = clientId });

                Console.WriteLine("received sound: " + soundLevel);
            }

            if (_sensorDataList.Count() == 2)
                await SaveAllSensorDataAsync();
        }

        private async Task SaveAllSensorDataAsync()
        {
                 foreach (var sensorData in _sensorDataList)
                {

                    // Check if ESP32 device exists
                    var espDevice = await _esp32DeviceService.GetDeviceByIdAsync(sensorData.ClientId);
                    if (espDevice == null)
                    {
                        // Create a new ESP32 device
                        espDevice = new ESP32_Device { DeviceID = sensorData.ClientId, DeviceName = "ESP32WROOM32", DeviceType = "ESP32", Location = "Corridor" };
                        await _esp32DeviceService.AddDeviceAsync(espDevice);
                    }
                 
                    if (sensorData.PIRSensorValue != null)
                    {
                        // Create a new PIRSensor entity
                        var pirSensor = new PIRSensor
                        {
                            DeviceID = espDevice.DeviceID,
                            SensorID = 0
                        };

                        // Add the new PIRSensor entity to the database
                        await _pirSensorService.AddPIRSensorAsync(pirSensor);

                        // Create a new SensorActivityLog entity
                        var sensorActivityLog = new SensorActivityLog
                        {

                            DeviceID = espDevice.DeviceID,
                            PIRSensorID = pirSensor.SensorID,
                            SoundSensorID = 0,
                            Timestamp = sensorData.Timestamp,
                            SoundLevel = 120,
                            Distance = 20
                        };

                        // Add the new SensorActivityLog entity to the database
                        await _sensorActivityLogService.AddSensorActivityLogAsync(sensorActivityLog);
                    }

                    if (sensorData.SoundSensorLevel != null)
                    {
                        // Create a new SoundSensor entity
                        var soundSensor = new SoundSensor
                        {
                            DeviceID = espDevice.DeviceID,
                            SensorID = 0
                        };

                        // Add the new SoundSensor entity to the database
                        await _soundSensorService.AddSoundSensorAsync(soundSensor);

                        // Create a new SensorActivityLog entity
                        var sensorActivityLog = new SensorActivityLog
                        {
                            DeviceID = espDevice.DeviceID,
                            PIRSensorID = 0,
                            SoundSensorID = soundSensor.SensorID,
                            Timestamp = sensorData.Timestamp,
                            SoundLevel = sensorData.SoundSensorLevel,
                            Distance = 20
                        };

                        // Add the new SensorActivityLog entity to the database
                        await _sensorActivityLogService.AddSensorActivityLogAsync(sensorActivityLog);
                    }
                }

                // Clear the list after processing
                _sensorDataList.Clear();
         }

        public Task<SensorData> GetLatestSensorDataAsync()
        {
            return Task.FromResult(_sensorDataList.LastOrDefault());
        }
    }

    public class MqttBackgroundService : BackgroundService
    {
        private readonly IMqttService _mqttService;
        private readonly IConfiguration _configuration;
        private readonly IServiceScope _scope;

        public MqttBackgroundService(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _scope = scopeFactory.CreateScope();
            {
                _mqttService = _scope.ServiceProvider.GetRequiredService<IMqttService>();
            }

            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var brokerUrl = _configuration["MQTT:BrokerUrl"];
            var clientId = _configuration["MQTT:ClientId"];
            var username = _configuration["MQTT:Username"];
            var password = _configuration["MQTT:Password"];

            await _mqttService.StartAsync(brokerUrl, clientId, username, password);
            stoppingToken.Register(() => _mqttService.DisconnectAsync().Wait());
        }
    }
}
