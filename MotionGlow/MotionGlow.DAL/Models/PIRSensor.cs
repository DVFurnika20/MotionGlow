using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotionGlow.DAL.Models
{
    public partial class PIRSensor
    {
        private int _sensorId;
        private int? _deviceId;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SensorID
        {
            get { return _sensorId; }
            set { _sensorId = value; }
        }

        public int? DeviceID
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        [ForeignKey("DeviceID")]
        [InverseProperty("PIRSensor")]
        public virtual ESP32_Device Device { get; set; }

        [InverseProperty("PIRSensor")]
        public virtual ICollection<SensorActivityLog> SensorActivityLog { get; set; } = new List<SensorActivityLog>();
    }
}