using Microsoft.AspNetCore.Mvc;
using MotionGlow.BLL.Services;
using MotionGlow.DAL.Models;

namespace MotionGlow.Controllers
{
    public class SensorController : Controller
    {
        private readonly IMqttService _mqttService;

        public SensorController(IMqttService mqttService)
        {
            _mqttService = mqttService;
        }

        public async Task<IActionResult> Index()
        {
            // Retrieve sensor data using the MqttService
            var sensorData = await _mqttService.GetLatestSensorDataAsync();

            // Create a view model to pass the sensor data to the view
            var viewModel = new SensorData
            {
                PIRSensorValue = sensorData.PIRSensorValue,
                SoundSensorLevel = sensorData.SoundSensorLevel,
                Timestamp = sensorData.Timestamp
            };

            // Pass the view model to the view
            return View(viewModel);
        }
    }
}
