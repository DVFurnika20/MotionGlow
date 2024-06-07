using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MotionGlow.DAL.Models;

public partial class SoundSensor
{
    [Key]
    public int SensorID { get; set; }

    public int DeviceID { get; set; }

    [ForeignKey("DeviceID")]
    [InverseProperty("SoundSensor")]
    public virtual ESP32_Device Device { get; set; } = null!;

    [InverseProperty("SoundSensor")]
    public virtual ICollection<SensorActivityLog> SensorActivityLog { get; set; } = new List<SensorActivityLog>();
}
