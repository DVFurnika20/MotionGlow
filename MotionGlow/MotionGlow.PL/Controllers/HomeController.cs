using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotionGlow.Models;
using MotionGlow.BLL.IServices;
using System.Threading.Tasks;

namespace MotionGlow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IESP32_DeviceService _deviceService;
        private readonly IPIRSensorService _pirSensorService;
        private readonly ISensorActivityLogService _activityLogService;
        private readonly ISoundSensorService _soundSensorService;

        public HomeController(
            ILogger<HomeController> logger,
            IESP32_DeviceService deviceService,
            IPIRSensorService pirSensorService,
            ISensorActivityLogService activityLogService,
            ISoundSensorService soundSensorService)
        {
            _logger = logger;
            _deviceService = deviceService;
            _pirSensorService = pirSensorService;
            _activityLogService = activityLogService;
            _soundSensorService = soundSensorService;
        }

        public async Task<IActionResult> ESP32_DeviceDetails(int id)
        {
            var device = await _deviceService.GetDeviceByIdAsync(id);
            if (device == null)
            {
                return View("Views/ESP32_DeviceDetails/ESP32_DeviceDetails.cshtml");
            }

            var viewModel = new ESP32_DeviceViewModel
            {
                DeviceName = device.DeviceName,
                DeviceType = device.DeviceType,
                Location = device.Location,
                Description = device.Description
            };
            return NotFound();
        }

        public async Task<IActionResult> PIRSensorDetails(int id)
        {
            var pirSensor = await _pirSensorService.GetPIRSensorByIdAsync(id);
            if (pirSensor == null)
            {
                return View("Views/PIRSensorDetails/PIRSensorDetails.cshtml");
            }

            var viewModel = new PIRSensorViewModel
            {
                Device = new ESP32_DeviceViewModel
                {
                    DeviceName = pirSensor.Device.DeviceName,
                    DeviceType = pirSensor.Device.DeviceType,
                    Location = pirSensor.Device.Location,
                    Description = pirSensor.Device.Description
                }
            };
            return NotFound();
        }

        public async Task<IActionResult> SensorActivityLogDetails(int id)
        {
            var activityLog = await _activityLogService.GetSensorActivityLogByIdAsync(id);
            if (activityLog == null)
            {
                return View("Views/SensorActivityLogDetails/SensorActivityLogDetails.cshtml");
            }

            var viewModel = new SensorActivityLogViewModel
            {
                Timestamp = activityLog.Timestamp,
                SoundLevel = activityLog.SoundLevel,
                Distance = activityLog.Distance,
                Device = new ESP32_DeviceViewModel
                {
                    DeviceName = activityLog.Device.DeviceName,
                    DeviceType = activityLog.Device.DeviceType,
                    Location = activityLog.Device.Location,
                    Description = activityLog.Device.Description
                },
                PIRSensor = new PIRSensorViewModel
                {
                    Device = new ESP32_DeviceViewModel
                    {
                        DeviceName = activityLog.PIRSensor.Device.DeviceName,
                        DeviceType = activityLog.PIRSensor.Device.DeviceType,
                        Location = activityLog.PIRSensor.Device.Location,
                        Description = activityLog.PIRSensor.Device.Description
                    }
                },
                SoundSensor = new SoundSensorViewModel
                {
                    Device = new ESP32_DeviceViewModel
                    {
                        DeviceName = activityLog.SoundSensor.Device.DeviceName,
                        DeviceType = activityLog.SoundSensor.Device.DeviceType,
                        Location = activityLog.SoundSensor.Device.Location,
                        Description = activityLog.SoundSensor.Device.Description
                    }
                }
            };
            return NotFound();
        }

        public async Task<IActionResult> SoundSensorDetails(int id)
        {
            var soundSensor = await _soundSensorService.GetSoundSensorByIdAsync(id);
            if (soundSensor == null)
            {
                return View("Views/SoundSensorDetails/SoundSensorDetails.cshtml");
            }

            var viewModel = new SoundSensorViewModel
            {
                Device = new ESP32_DeviceViewModel
                {
                    DeviceName = soundSensor.Device.DeviceName,
                    DeviceType = soundSensor.Device.DeviceType,
                    Location = soundSensor.Device.Location,
                    Description = soundSensor.Device.Description
                }
            };
            return NotFound();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}