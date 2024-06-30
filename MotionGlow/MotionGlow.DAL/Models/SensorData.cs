namespace MotionGlow.DAL.Models
{
    public class SensorData
    {
        public bool PIRSensorValue { get; set; }
        public int SoundSensorLevel { get; set; }
        public DateTime Timestamp { get; set; }
        public int ClientId { get; set; }
    }
}
