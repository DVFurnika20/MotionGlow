using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotionGlow.DAL.Models
{
    public partial class SensorActivityLog
    {
        private int _logId;
        private int? _deviceId;
        private int? _soundSensorId;
        private int? _pirSensorId;
        private DateTime? _timestamp;
        private int? _soundLevel;
        private decimal? _distance;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogID
        {
            get { return _logId; }
            set { _logId = value; }
        }

        public int? DeviceID
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        public int? SoundSensorID
        {
            get { return _soundSensorId; }
            set { _soundSensorId = value; }
        }

        public int? PIRSensorID
        {
            get { return _pirSensorId; }
            set { _pirSensorId = value; }
        }

        [Column(TypeName = "datetime")]
        public DateTime? Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        public int? SoundLevel
        {
            get { return _soundLevel; }
            set { _soundLevel = value; }
        }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        [ForeignKey("DeviceID")]
        [InverseProperty("SensorActivityLog")]
        public virtual ESP32_Device Device { get; set; }

        [ForeignKey("PIRSensorID")]
        [InverseProperty("SensorActivityLog")]
        public virtual PIRSensor PIRSensor { get; set; }

        [ForeignKey("SoundSensorID")]
        [InverseProperty("SensorActivityLog")]
        public virtual SoundSensor SoundSensor { get; set; }
    }
}