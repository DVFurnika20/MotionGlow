using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MotionGlow.DAL.Models;

public partial class PIRSensor
{
    [Key]
    public int SensorID { get; set; }

    public int? DeviceID { get; set; }

    [ForeignKey("DeviceID")]
    [InverseProperty("PIRSensor")]
    public virtual ESP32_Device? Device { get; set; }

    [InverseProperty("PIRSensor")]
    public virtual ICollection<SensorActivityLog> SensorActivityLog { get; set; } = new List<SensorActivityLog>();
}
