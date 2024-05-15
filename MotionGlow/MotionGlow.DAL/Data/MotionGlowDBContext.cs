using Microsoft.EntityFrameworkCore;
using MotionGlow.DAL.Models;

namespace MotionGlow.DAL.Data;

public partial class MotionGlowDbContext : DbContext
{
    public MotionGlowDbContext(DbContextOptions<MotionGlowDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ESP32_Device> ESP32_Device { get; set; }

    public virtual DbSet<PIRSensor> PIRSensor { get; set; }

    public virtual DbSet<SensorActivityLog> SensorActivityLog { get; set; }

    public virtual DbSet<SoundSensor> SoundSensor { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ESP32_Device>(entity =>
        {
            entity.HasKey(e => e.DeviceID).HasName("PK__ESP32_De__49E123317F6364D6");

            entity.Property(e => e.DeviceID).ValueGeneratedNever();
        });

        modelBuilder.Entity<PIRSensor>(entity =>
        {
            entity.HasKey(e => e.SensorID).HasName("PK__PIRSenso__D809841AE0FAE2E3");

            entity.Property(e => e.SensorID).ValueGeneratedNever();

            entity.HasOne(d => d.Device).WithMany(p => p.PIRSensor).HasConstraintName("FK__PIRSensor__Devic__4E88ABD4");
        });

        modelBuilder.Entity<SensorActivityLog>(entity =>
        {
            entity.HasKey(e => e.LogID).HasName("PK__SensorAc__5E5499A8770AC7AC");

            entity.Property(e => e.LogID).ValueGeneratedNever();

            entity.HasOne(d => d.Device).WithMany(p => p.SensorActivityLog).HasConstraintName("FK__SensorAct__Devic__5165187F");

            entity.HasOne(d => d.PIRSensor).WithMany(p => p.SensorActivityLog).HasConstraintName("FK__SensorAct__PIRSe__534D60F1");

            entity.HasOne(d => d.SoundSensor).WithMany(p => p.SensorActivityLog).HasConstraintName("FK__SensorAct__Sound__52593CB8");
        });

        modelBuilder.Entity<SoundSensor>(entity =>
        {
            entity.HasKey(e => e.SensorID).HasName("PK__SoundSen__D809841A97688CF6");

            entity.Property(e => e.SensorID).ValueGeneratedNever();

            entity.HasOne(d => d.Device).WithMany(p => p.SoundSensor).HasConstraintName("FK__SoundSens__Devic__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
