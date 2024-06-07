using System;
using System.Collections.Generic;
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
            entity.HasKey(e => e.DeviceID).HasName("PK__ESP32_De__49E1233103832F04");
        });

        modelBuilder.Entity<PIRSensor>(entity =>
        {
            entity.HasKey(e => e.SensorID).HasName("PK__PIRSenso__D809841A74EE4F65");

            entity.HasOne(d => d.Device).WithMany(p => p.PIRSensor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PIRSensor__Devic__74AE54BC");
        });

        modelBuilder.Entity<SensorActivityLog>(entity =>
        {
            entity.HasKey(e => e.LogID).HasName("PK__SensorAc__5E5499A86D16828D");

            entity.HasOne(d => d.Device).WithMany(p => p.SensorActivityLog)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SensorAct__Devic__778AC167");

            entity.HasOne(d => d.PIRSensor).WithMany(p => p.SensorActivityLog)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SensorAct__PIRSe__797309D9");

            entity.HasOne(d => d.SoundSensor).WithMany(p => p.SensorActivityLog)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SensorAct__Sound__787EE5A0");
        });

        modelBuilder.Entity<SoundSensor>(entity =>
        {
            entity.HasKey(e => e.SensorID).HasName("PK__SoundSen__D809841A09F0AE66");

            entity.HasOne(d => d.Device).WithMany(p => p.SoundSensor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SoundSens__Devic__71D1E811");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
