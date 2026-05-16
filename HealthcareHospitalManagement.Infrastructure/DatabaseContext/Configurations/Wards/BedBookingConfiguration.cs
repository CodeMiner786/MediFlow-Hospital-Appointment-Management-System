using HealthcareHospitalManagement.Domain.Entities.Wards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Wards;

public class BedBookingConfiguration : IEntityTypeConfiguration<BedBooking>
{
    public void Configure(EntityTypeBuilder<BedBooking> builder)
    {
        // ── ১. টেবিল ও স্কিমা কনফিগারেশন ──────────────────────────────────────────────────
        builder.ToTable("BedBookings", "Ward", t =>
            t.HasComment("ইউজারদের সরাসরি অ্যাপ থেকে বেড বুকিং এবং পেমেন্ট সংক্রান্ত তথ্য।"));

        // ── ২. কি এবং ইউনিকনেস ────────────────────────────────────────────────────────────
        builder.HasKey(b => b.Id);

        builder.Property(b => b.BookingCode)
               .IsRequired()
               .HasMaxLength(30);

        builder.HasIndex(b => b.BookingCode).IsUnique();

        // ── ৩. প্রপার্টি কনফিগারেশন ও ডেসিমাল প্রিসিশন ──────────────────────────────
        // ডেসিমাল ওয়ার্নিং (Precision) সমাধান করতে
        builder.Property(b => b.TotalCharge)
               .HasPrecision(18, 2);

        builder.Property(b => b.SpecialRequests).HasMaxLength(500);
        builder.Property(b => b.Notes).HasMaxLength(1000);

        // এনাম কনভার্সন (স্ট্রিং হিসেবে সেভ হবে)
        builder.Property(b => b.BookingStatus)
               .HasConversion<string>()
               .HasMaxLength(30);

        // ── ৪. রিলেশনশিপ (Shadow Property ফিক্স) ──────────────────────────

        // ক. Bed এর সাথে রিলেশন (BedId1 রোধে)
        builder.HasOne(b => b.Bed)
               .WithMany(bed => bed.Bookings) // Bed এনটিটিতে ICollection<BedBooking> Bookings থাকলে এটি দিন
               .HasForeignKey(b => b.BedId)
               .OnDelete(DeleteBehavior.Restrict);

        // খ. Patient এর সাথে রিলেশন (Guid? হওয়ার কারণে এটি অপশনাল)
        builder.HasOne(b => b.Patient)
               .WithMany()
               .HasForeignKey(b => b.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        // গ. BookedBy User এর সাথে রিলেশন (ApplicationUser)
        builder.HasOne(b => b.BookedByUser)
               .WithMany()
               .HasForeignKey(b => b.BookedByUserId)
               .OnDelete(DeleteBehavior.Restrict);

        // ── ৫. ইন্ডেক্সিং ও ডিফল্ট ভ্যালু ──────────────────────────────────────────────
        builder.Property(b => b.IsPaid).HasDefaultValue(false);
        builder.HasIndex(b => b.CheckInDate);
        builder.HasIndex(b => b.IsPaid);
        builder.HasIndex(b => b.BookedByUserId);
    }
}