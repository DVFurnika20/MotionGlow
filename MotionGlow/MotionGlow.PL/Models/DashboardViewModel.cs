namespace MotionGlow.Models;

public class DashboardViewModel
{
    public IEnumerable<ESP32_DeviceViewModel> ESP32_Devices { get; set; }
    public IEnumerable<PIRSensorViewModel> PIRSensors { get; set; }
    public IEnumerable<SensorActivityLogViewModel> SensorActivityLogs { get; set; }
    public IEnumerable<SoundSensorViewModel> SoundSensors { get; set; }
}