namespace MotionGlow.Models;

public class SensorActivityLogViewModel
{
    public DateTime Timestamp { get; set; }
    public int? SoundLevel { get; set; }
    public decimal? Distance { get; set; }
    public int DeviceId { get; set; }
    public ESP32_DeviceViewModel Device { get; set; }
}