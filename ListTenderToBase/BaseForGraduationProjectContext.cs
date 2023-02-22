using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ListTenderToBase;

public partial class BaseForGraduationProjectContext : DbContext
{
    public BaseForGraduationProjectContext()
    {
    }

    public BaseForGraduationProjectContext(DbContextOptions<BaseForGraduationProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Platform> Platforms { get; set; }

    public virtual DbSet<Procurement> Procurements { get; set; }

    public virtual DbSet<TimeZone> TimeZones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ngknn.ru;Database=BaseForGraduationProject;User ID = 33П; Password = 12357; TrustServerCertificate = true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("PK_Organisation");
        });

        modelBuilder.Entity<Procurement>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DeadlineEnd).HasPrecision(0);
            entity.Property(e => e.DeadlineStart).HasPrecision(0);
            entity.Property(e => e.InitialPrice).HasColumnType("decimal(19, 2)");
            entity.Property(e => e.ProvidingAguarantee).HasColumnName("ProvidingAGuarantee");

            entity.HasOne(d => d.Organization).WithMany(p => p.Procurements)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK_Procurements_Organizations");

            entity.HasOne(d => d.Platform).WithMany(p => p.Procurements)
                .HasForeignKey(d => d.PlatformId)
                .HasConstraintName("FK_Procurements_Platforms");

            entity.HasOne(d => d.TimeZone).WithMany(p => p.Procurements)
                .HasForeignKey(d => d.TimeZoneId)
                .HasConstraintName("FK_Procurements_TimeZones");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
