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
        private readonly ISensorActivityLogService _sensorActivityLogService;

        public HomeController(ILogger<HomeController> logger, ISensorActivityLogService sensorActivityLogService)
        {
            _logger = logger;
            _sensorActivityLogService = sensorActivityLogService;
        }

        public async Task<IActionResult> Index()
        {
            var sensorActivityLogs = await _sensorActivityLogService.GetAllSensorActivityLogsAsync();

            var totalCount = sensorActivityLogs.Count();
            var logsPerDevice = sensorActivityLogs.GroupBy(log => log.DeviceID)
                .Select(group => new { DeviceID = group.Key, Count = group.Count() })
                .ToDictionary(x => x.DeviceID.ToString(), x => x.Count);

            var today = DateTime.Now.Date;
            var soundLevelsToday = sensorActivityLogs
                .Where(log => log.Timestamp.Date == today)
                .GroupBy(log => log.Timestamp.Hour)
                .Select(group => new { Hour = group.Key, AverageSoundLevel = group.Average(log => log.SoundLevel) })
                .ToDictionary(x => x.Hour.ToString(), x => x.AverageSoundLevel);

            var startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);
            var logsPerDayOfWeek = sensorActivityLogs
                .Where(log => log.Timestamp.Date >= startOfWeek && log.Timestamp.Date < endOfWeek)
                .GroupBy(log => log.Timestamp.DayOfWeek)
                .Select(group => new { DayOfWeek = group.Key, Count = group.Count() })
                .ToDictionary(x => x.DayOfWeek.ToString(), x => x.Count);

            ViewData["LogsPerDayOfWeek"] = logsPerDayOfWeek;
            ViewData["SoundLevelsToday"] = soundLevelsToday;
            ViewData["TotalCount"] = totalCount;
            ViewData["LogsPerDevice"] = logsPerDevice;

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