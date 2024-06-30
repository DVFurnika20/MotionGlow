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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionGlow.BLL.Services
{
    public class MqttService : IMqttService
    {
        private readonly IPIRSensorService _pirSensorService;
        private readonly ISoundSensorService _soundSensorService;
        private readonly IESP32_DeviceService _esp32DeviceService;
        private readonly ISensorActivityLogService _sensorActivityLogService;
        private readonly IMqttClient _mqttClient;
        private SensorData _latestSensorData;

        public MqttService(
            IPIRSensorService pirSensorService,
            ISoundSensorService soundSensorService,
            IESP32_DeviceService esp32DeviceService,
            ISensorActivityLogService sensorActivityLogService,
            IConfiguration configuration)
        {
            _pirSensorService = pirSensorService;
            _soundSensorService = soundSensorService;
            _esp32DeviceService = esp32DeviceService;
            _sensorActivityLogService = sensorActivityLogService;

            // Create a new MQTT client
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

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

            await _mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("pir_sensor").Build());
            await _mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("sound_sensor").Build());
            await _mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("client_id").Build());
        }

        public async Task DisconnectAsync()
        {
            await _mqttClient.DisconnectAsync();
        }

        private async void ProcessReceivedMessage(MqttApplicationMessage message)
        {
            string topic = message.Topic;
            byte[] payload = message.Payload;

            if (topic == "pir_sensor")
            {
                bool pirValue = (Convert.ToInt32(Encoding.UTF8.GetString(payload)) == 1);
                await HandlePIRSensorDataAsync(pirValue);
            }
            else if (topic == "sound_sensor")
            {
                int soundLevel = Convert.ToInt32(Encoding.UTF8.GetString(payload));
                await HandleSoundSensorDataAsync(soundLevel);
            }
            else if (topic == "client_id")
            {
                int clientId = Convert.ToInt32(Encoding.UTF8.GetString(payload));
                await HandleClientIdDataAsync(clientId);
            }
        }

        private async Task HandlePIRSensorDataAsync(bool pirValue)
        {
            // Check if ESP32 device exists
            var espDevice = await _esp32DeviceService.GetDeviceByIdAsync(/*_latestSensorData?.ClientId ?? 0*/ 0);
            if (espDevice == null)
            {
                // Create a new ESP32 device
                espDevice = new ESP32_Device { DeviceID = _latestSensorData?.ClientId ?? 0 };
                await _esp32DeviceService.AddDeviceAsync(espDevice);
            }

            // Create a new PIRSensor entity
            var pirSensor = new PIRSensor
            {
                DeviceID = espDevice.DeviceID,
                SensorID = 0 // New entity
            };

            // Add the new PIRSensor entity to the database
            await _pirSensorService.AddPIRSensorAsync(pirSensor);

            // Update the latest sensor data
            UpdateLatestSensorData(pirValue, _latestSensorData?.SoundSensorLevel ?? 0, DateTime.Now, espDevice.DeviceID);

            // Create a new SensorActivityLog entity
            var sensorActivityLog = new SensorActivityLog
            {
                DeviceID = espDevice.DeviceID,
                PIRSensorID = pirSensor.SensorID,
                SoundSensorID = 6, // Replace with actual SoundSensorID
                Timestamp = DateTime.UtcNow,
                SoundLevel = null, // Not logging sound sensor data
                Distance = null // Not logging distance data
            };

            // Add the new SensorActivityLog entity to the database
            await _sensorActivityLogService.AddSensorActivityLogAsync(sensorActivityLog);

            Console.WriteLine("PIR sensor value " + pirValue);
        }

        private async Task HandleSoundSensorDataAsync(int soundLevel)
        {
            // Check if ESP32 device exists
            var espDevice = await _esp32DeviceService.GetDeviceByIdAsync(/*_latestSensorData?.ClientId ?? 0*/ 0);
            if (espDevice == null)
            {
                // Create a new ESP32 device
                espDevice = new ESP32_Device { DeviceID = _latestSensorData?.ClientId ?? 0 };
                await _esp32DeviceService.AddDeviceAsync(espDevice);
            }

            // Create a new SoundSensor entity
            var soundSensor = new SoundSensor
            {
                DeviceID = espDevice.DeviceID,
                SensorID = 0 // New entity
            };

            // Add the new SoundSensor entity to the database
            await _soundSensorService.AddSoundSensorAsync(soundSensor);

            // Update the latest sensor data
            UpdateLatestSensorData(_latestSensorData?.PIRSensorValue ?? false, soundLevel, DateTime.Now, espDevice.DeviceID);

            // Create a new SensorActivityLog entity
            var sensorActivityLog = new SensorActivityLog
            {
                DeviceID = espDevice.DeviceID,
                PIRSensorID = 6, // Replace with actual PIRSensorID
                SoundSensorID = soundSensor.SensorID,
                Timestamp = DateTime.UtcNow,
                SoundLevel = soundLevel,
                Distance = null // Not logging distance data
            };

            // Add the new SensorActivityLog entity to the database
            await _sensorActivityLogService.AddSensorActivityLogAsync(sensorActivityLog);

            Console.WriteLine("Sound sensor value " + soundLevel);
        }

        private async Task HandleClientIdDataAsync(int clientId)
        {
            // Update the latest sensor data with the client ID
            UpdateLatestSensorData(_latestSensorData?.PIRSensorValue ?? false, _latestSensorData?.SoundSensorLevel ?? 0, DateTime.Now, clientId);

            Console.WriteLine("ClientId value " + clientId);
        }

        private void UpdateLatestSensorData(bool pirValue, int soundLevel, DateTime timestamp, int clientId)
        {
            _latestSensorData = new SensorData
            {
                PIRSensorValue = pirValue,
                SoundSensorLevel = soundLevel,
                Timestamp = timestamp,
                ClientId = clientId
            };
        }

        public Task<SensorData> GetLatestSensorDataAsync()
        {
            return Task.FromResult(_latestSensorData);
        }
    }

    public class MqttBackgroundService : BackgroundService
    {
        private readonly IMqttService _mqttService;
        private readonly IConfiguration _configuration;

        public MqttBackgroundService(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                _mqttService = scope.ServiceProvider.GetRequiredService<IMqttService>();
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
