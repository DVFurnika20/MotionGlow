using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MotionGlow.DAL.Data;
using MotionGlow.DAL.Models;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace MotionGlow.BLL.Services
{
    public class MqttService : IMqttService
    {
        private readonly MotionGlowDbContext _dbContext;
        private readonly IMqttClient _mqttClient;
        private SensorData _latestSensorData;

        public MqttService(MotionGlowDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;

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

            // Connect to HiveMQ cluster
            await _mqttClient.ConnectAsync(options);

            // Subscribe to MQTT topics
            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                // Process the received message
                ProcessReceivedMessage(e.ApplicationMessage);
            });

            await _mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("pir_sensor").Build());
            await _mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("sound_sensor").Build());
        }

        public async Task DisconnectAsync()
        {
            await _mqttClient.DisconnectAsync();
        }

        private void ProcessReceivedMessage(MqttApplicationMessage message)
        {
            string topic = message.Topic;
            byte[] payload = message.Payload;

            // Process the received message based on the topic
            if (topic == "pir_sensor")
            {
                bool pirValue = (Convert.ToInt32(System.Text.Encoding.UTF8.GetString(payload)) == 1);
                
                // Update the latest sensor data
                UpdateLatestSensorData(pirValue, _latestSensorData?.SoundSensorLevel ?? 0, DateTime.Now, _latestSensorData?.ClientId ?? 0);

                // Save PIR sensor data to the database
                SavePIRSensorDataAsync(pirValue);

                Console.WriteLine("PIR sensor value " + pirValue);
            }
            else if (topic == "sound_sensor")
            {
                int soundLevel = Convert.ToInt32(System.Text.Encoding.UTF8.GetString(payload));
                // Update the latest sensor data
                UpdateLatestSensorData(_latestSensorData?.PIRSensorValue ?? false, soundLevel, DateTime.Now, _latestSensorData?.ClientId ?? 0);

                // Save sound sensor data to the database
                SaveSoundSensorDataAsync(soundLevel);

                Console.WriteLine("Sound sensor value " + soundLevel);
            }
            else if (topic == "client_id")
            {
                int ClientId = Convert.ToInt32(System.Text.Encoding.UTF8.GetString(payload));
                // Update the latest sensor data
                UpdateLatestSensorData(_latestSensorData?.PIRSensorValue ?? false, _latestSensorData?.SoundSensorLevel ?? 0, DateTime.Now, ClientId);

                Console.WriteLine("ClientId value " + ClientId);
            }
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

            //new 
        }

        public Task<SensorData> GetLatestSensorDataAsync()
        {
            return Task.FromResult(_latestSensorData);
        }

        private async Task SavePIRSensorDataAsync(bool pirValue)
        {
            // Create a new PIRSensor entity
            var pirSensor = new PIRSensor
            {
                DeviceID = 6,
                SensorID = 0 // Set the SensorID to 0 for a new entity
            };

            // Add the new PIRSensor entity to the database
            await _dbContext.PIRSensor.AddAsync(pirSensor);
            await _dbContext.SaveChangesAsync();

            // Create a new SensorActivityLog entity
            var sensorActivityLog = new SensorActivityLog
            {
                DeviceID = pirSensor.DeviceID,
                PIRSensorID = pirSensor.SensorID,
                SoundSensorID = 6,
                Timestamp = DateTime.UtcNow,
                SoundLevel = null, // Set the sound level to null since we're only logging PIR sensor data
                Distance = null // Set the distance to null since we're only logging PIR sensor data
            };

            // Add the new SensorActivityLog entity to the database
            await _dbContext.SensorActivityLog.AddAsync(sensorActivityLog);
            await _dbContext.SaveChangesAsync();
        }

        private async Task SaveSoundSensorDataAsync(int soundLevel)
        {
            // Create a new SoundSensor entity
            var soundSensor = new SoundSensor
            {
                DeviceID = 6, // Replace with the actual device ID
                SensorID = 0 // Set the SensorID to 0 for a new entity
            };

            // Add the new SoundSensor entity to the database
            await _dbContext.SoundSensor.AddAsync(soundSensor);
            await _dbContext.SaveChangesAsync();

            // Create a new SensorActivityLog entity
            var sensorActivityLog = new SensorActivityLog
            {
                DeviceID = soundSensor.DeviceID,
                PIRSensorID = 6, 
                SoundSensorID = soundSensor.SensorID,
                Timestamp = DateTime.UtcNow,
                SoundLevel = soundLevel,
                Distance = null // Set the distance to null since we're only logging sound sensor data
            };

            // Add the new SensorActivityLog entity to the database
            await _dbContext.SensorActivityLog.AddAsync(sensorActivityLog);
            await _dbContext.SaveChangesAsync();
        }

    }

    public class MqttBackgroundService : BackgroundService
    {
        private readonly IMqttService _mqttService;
        private readonly IConfiguration _configuration;

        public MqttBackgroundService(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            Console.WriteLine("\n\nwhatttn");
            using (var scope = scopeFactory.CreateScope())
            {
                _mqttService = scope.ServiceProvider.GetRequiredService<IMqttService>();
            }
 ///**/               _mqttService = new MqttService(dbContext, configuration);
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

            Console.WriteLine("connected");
        }
    }
}
