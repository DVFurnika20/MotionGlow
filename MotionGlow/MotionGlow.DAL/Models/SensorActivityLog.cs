using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MotionGlow.DAL.Models;

public partial class SensorActivityLog
{
    [Key]
    public int LogID { get; set; }

    public int? DeviceID { get; set; }

    public int? SoundSensorID { get; set; }

    public int? PIRSensorID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Timestamp { get; set; }

    public int? SoundLevel { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Distance { get; set; }

    [ForeignKey("DeviceID")]
    [InverseProperty("SensorActivityLog")]
    public virtual ESP32_Device? Device { get; set; }

    [ForeignKey("PIRSensorID")]
    [InverseProperty("SensorActivityLog")]
    public virtual PIRSensor? PIRSensor { get; set; }

    [ForeignKey("SoundSensorID")]
    [InverseProperty("SensorActivityLog")]
    public virtual SoundSensor? SoundSensor { get; set; }
}
