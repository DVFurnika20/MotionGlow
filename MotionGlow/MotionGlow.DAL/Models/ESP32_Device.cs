using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MotionGlow.DAL.Models;

public partial class ESP32_Device
{
    [Key]
    public int DeviceID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? DeviceName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? DeviceType { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Location { get; set; }

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [InverseProperty("Device")]
    public virtual ICollection<PIRSensor> PIRSensor { get; set; } = new List<PIRSensor>();

    [InverseProperty("Device")]
    public virtual ICollection<SensorActivityLog> SensorActivityLog { get; set; } = new List<SensorActivityLog>();

    [InverseProperty("Device")]
    public virtual ICollection<SoundSensor> SoundSensor { get; set; } = new List<SoundSensor>();
}
