using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotionGlow.Models;
using MotionGlow.BLL.IServices;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotionGlow.Controllers
{
    public class DetailsController : Controller
    {
        private readonly IESP32_DeviceService _deviceService;
        private readonly ISensorActivityLogService _activityLogService;

        public DetailsController(IESP32_DeviceService deviceService, ISensorActivityLogService activityLogService)
        {
            _deviceService = deviceService;
            _activityLogService = activityLogService;
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
    }
}