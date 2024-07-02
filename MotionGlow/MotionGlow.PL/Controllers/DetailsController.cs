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

            return View("~/Views/ESP32_DeviceDetails/ESP32_DeviceDetails.cshtml", viewModels);
        }

        public async Task<IActionResult> SensorActivityLogDetails(string dateFilter, string soundLevelFilter, string distanceFilter)
        {
            var activityLogs = await _activityLogService.GetAllSensorActivityLogsAsync();

            // Order by date descending by default
            activityLogs = activityLogs.OrderByDescending(log => log.Timestamp);
            
            // Apply filters here
            if (dateFilter == "MostRecent")
            {
                // No action needed, already ordered by date descending
            }
            else if (dateFilter == "Oldest")
            {
                activityLogs = activityLogs.OrderBy(log => log.Timestamp);
            }

            if (soundLevelFilter == "Highest")
            {
                activityLogs = activityLogs.OrderByDescending(log => log.SoundLevel);
            }
            else if (soundLevelFilter == "Lowest")
            {
                activityLogs = activityLogs.OrderBy(log => log.SoundLevel);
            }
            else if (soundLevelFilter == "None")
            {
                // No action needed, no filter applied
            }

            if (distanceFilter == "Farthest")
            {
                activityLogs = activityLogs.OrderByDescending(log => log.Distance);
            }
            else if (distanceFilter == "Closest")
            {
                activityLogs = activityLogs.OrderBy(log => log.Distance);
            }
            else if (distanceFilter == "None")
            {
                // No action needed, no filter applied
            }

            ViewBag.DateFilter = dateFilter;
            ViewBag.SoundLevelFilter = soundLevelFilter;
            ViewBag.DistanceFilter = distanceFilter;

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

            return View("~/Views/SensorActivityLogDetails/SensorActivityLogDetails.cshtml", viewModels);
        }
    }
}
