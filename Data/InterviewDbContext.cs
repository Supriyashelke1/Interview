using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Interview.Data;

public partial class InterviewDbContext : DbContext
{
    public InterviewDbContext()
    {
    }

    public InterviewDbContext(DbContextOptions<InterviewDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=tcp:mysqlserver8600.database.windows.net,1433;Initial Catalog=InterviewDb;Persist Security Info=False;User ID=azureuser;Password=Capg@8600;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3214EC077D84D1CD");

            entity.ToTable("Appointment");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Purpose).HasMaxLength(50);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking__3214EC074A67909E");

            entity.ToTable("Booking");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BookingDate).HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK_Bookings_Appointments");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROLE__3214EC072C854A97");

            entity.ToTable("ROLE");

            entity.Property(e => e.Role1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserLogi__3214EC07F5E58D6A");

            entity.ToTable("UserLogin");

            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
