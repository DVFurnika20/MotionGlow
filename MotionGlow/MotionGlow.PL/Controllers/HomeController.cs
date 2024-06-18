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

        public async Task<IActionResult> ESP32_DeviceDetails()
        {
            var devices = await _deviceService.GetAllDevicesAsync();
            var viewModels = devices.Select(device => new ESP32_DeviceViewModel
            {
                DeviceName = device.DeviceName,
                DeviceType = device.DeviceType,
                Location = device.Location,
                Description = device.Description
            }).ToList();

            return View("Views/ESP32_DeviceDetails/ESP32_DeviceDetails.cshtml", viewModels);
        }

        public async Task<IActionResult> SensorActivityLogDetails()
        {
            var activityLogs = await _activityLogService.GetAllSensorActivityLogsAsync();

            var viewModels = new List<SensorActivityLogViewModel>();

            foreach (var activityLog in activityLogs)
            {
                var device = await _deviceService.GetDeviceByIdAsync(activityLog.DeviceID);

                viewModels.Add(new SensorActivityLogViewModel
                {
                    Timestamp = activityLog.Timestamp,
                    SoundLevel = activityLog.SoundLevel,
                    Distance = activityLog.Distance,
                    DeviceId = activityLog.DeviceID,
                    Device = device != null ? new ESP32_DeviceViewModel
                    {
                        DeviceName = device.DeviceName,
                        DeviceType = device.DeviceType,
                        Location = device.Location,
                        Description = device.Description
                    } : null
                });
            }

            return View("Views/SensorActivityLogDetails/SensorActivityLogDetails.cshtml", viewModels);
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