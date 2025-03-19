using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlightBookAPI.Models;

public partial class FlightDemoContext : DbContext
{
    public FlightDemoContext()
    {
    }

    public FlightDemoContext(DbContextOptions<FlightDemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<CheckIn> CheckIns { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FlightDemo;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AED2890ED52");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_generate_reference");
                    tb.HasTrigger("trg_generate_referenceNo");
                });

            entity.HasIndex(e => e.ReferenceNumber, "UQ_ReferenceNumber").IsUnique();

            entity.HasIndex(e => e.ReferenceNumber, "UQ__Bookings__C5ADBE4D409D7D90").IsUnique();

            entity.Property(e => e.Adults).HasDefaultValue(1);
            entity.Property(e => e.BookingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ClassType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Economy");
            entity.Property(e => e.ReferenceNumber).HasMaxLength(50);
            entity.Property(e => e.SeatType).HasMaxLength(50);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

           
        });

        modelBuilder.Entity<CheckIn>(entity =>
        {
            entity.HasKey(e => e.CheckInId).HasName("PK__CheckIns__E649768401F7852C");

            entity.HasIndex(e => e.SeatNumber, "UQ__CheckIns__EE2DCB01AD7BFBDA").IsUnique();

            entity.Property(e => e.CheckInStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Checked-In");
            entity.Property(e => e.CheckInTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SeatNumber).HasMaxLength(10);

           
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flights__8A9E14EE77A4269F");

            entity.HasIndex(e => e.FlightNumber, "UQ__Flights__2EAE6F50F89AD102").IsUnique();

            entity.Property(e => e.Airline).HasMaxLength(100);
            entity.Property(e => e.AisleSeatPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.Destination).HasMaxLength(100);
            entity.Property(e => e.FlightImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FlightNumber).HasMaxLength(20);
            entity.Property(e => e.MiddleSeatPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Source).HasMaxLength(100);
            entity.Property(e => e.WindowSeatPrice).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A382286B433");

            entity.Property(e => e.AmountPaid).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Success");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

           

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__UserId__5441852A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CDBDE361A");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534637C427F").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("Passenger");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
