using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MotionGlow.DAL.Models
{
    public partial class ESP32_Device
    {
        private int _deviceId;
        private string _deviceName;
        private string _deviceType;
        private string _location;
        private string _description;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeviceID
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        [StringLength(100)]
        [Unicode(false)]
        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        [StringLength(50)]
        [Unicode(false)]
        public string DeviceType
        {
            get { return _deviceType; }
            set { _deviceType = value; }
        }

        [StringLength(100)]
        [Unicode(false)]
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        [Column(TypeName = "text")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        [InverseProperty("Device")]
        public virtual ICollection<PIRSensor> PIRSensor { get; set; } = new List<PIRSensor>();

        [InverseProperty("Device")]
        public virtual ICollection<SensorActivityLog> SensorActivityLog { get; set; } = new List<SensorActivityLog>();

        [InverseProperty("Device")]
        public virtual ICollection<SoundSensor> SoundSensor { get; set; } = new List<SoundSensor>();
    }
}