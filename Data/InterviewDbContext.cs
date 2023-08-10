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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
