using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Software_Account_Management.Models;

namespace Software_Account_Management.Data;

public partial class SoftwareLicenseServiceContext : DbContext
{
    public SoftwareLicenseServiceContext()
    {
    }

    public SoftwareLicenseServiceContext(DbContextOptions<SoftwareLicenseServiceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<License> Licenses { get; set; }

    public virtual DbSet<LicenseOrderBook> LicenseOrderBooks { get; set; }

    public virtual DbSet<LicenseVendor> LicenseVendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Software_Account_ManagementContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.ToTable("Application");

            entity.Property(e => e.ApplicationId)
                .ValueGeneratedNever()
                .HasColumnName("ApplicationID");
            entity.Property(e => e.ApplicationName).HasMaxLength(100);
            entity.Property(e => e.ApplicationVersion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.ToTable("License");

            entity.Property(e => e.LicenseId)
                .ValueGeneratedNever()
                .HasColumnName("LicenseID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ExpiredOn).HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.LicenseVendorId).HasColumnName("LicenseVendorID");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.SpaceId).HasColumnName("SpaceID");
            entity.Property(e => e.TestStationPool)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Application).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_License_Application");

            entity.HasOne(d => d.LicenseVendor).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.LicenseVendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_License_LicenseVendor");
        });

        modelBuilder.Entity<LicenseOrderBook>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("LicenseOrderBook");

            entity.Property(e => e.CompletionTime).HasColumnType("datetime");
            entity.Property(e => e.EstCompletionTime).HasColumnType("datetime");
            entity.Property(e => e.Framework)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
            entity.Property(e => e.LicenseOrderBookId).HasColumnName("LicenseOrderBookID");
            entity.Property(e => e.Orchestrator)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReservationTime).HasColumnType("datetime");
            entity.Property(e => e.ReservedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpaceId).HasColumnName("SpaceID");
            entity.Property(e => e.TestCaseId).HasColumnName("TestCaseID");
            entity.Property(e => e.TestStationName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TestStationPool)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.License).WithMany()
                .HasForeignKey(d => d.LicenseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LicenseOrderBook_License");
        });

        modelBuilder.Entity<LicenseVendor>(entity =>
        {
            entity.ToTable("LicenseVendor");

            entity.Property(e => e.LicenseVendorId)
                .ValueGeneratedNever()
                .HasColumnName("LicenseVendorID");
            entity.Property(e => e.VendorName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
