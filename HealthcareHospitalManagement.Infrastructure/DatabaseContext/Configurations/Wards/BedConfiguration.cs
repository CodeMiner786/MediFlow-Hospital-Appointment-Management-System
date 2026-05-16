using HealthcareHospitalManagement.Domain.Entities.Wards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Wards;

public class BedConfiguration : IEntityTypeConfiguration<Bed>
{
    public void Configure(EntityTypeBuilder<Bed> builder)
    {
        // ── ১. টেবিল ও স্কিমা কনফিগারেশন ──────────────────────────────────────────────────
        builder.ToTable("Beds", "Ward", t =>
            t.HasComment("হাসপাতালের ওয়ার্ডের অন্তর্গত প্রতিটি বেড এবং তার বর্তমান অবস্থার তথ্য।"));

        // ── ২. কি এবং ইউনিকনেস ────────────────────────────────────────────────────────────
        builder.HasKey(b => b.Id);

        builder.Property(b => b.BedNumber)
               .IsRequired()
               .HasMaxLength(30);

        // একই ওয়ার্ডে যেন একই বেড নাম্বার ডুপ্লিকেট না হয় তার জন্য ইউনিক ইন্ডেক্স
        builder.HasIndex(b => new { b.WardId, b.BedNumber }).IsUnique();

        // ── ৩. প্রপার্টি কনফিগারেশন ও ডেসিমাল প্রিসিশন ──────────────────────────────
        // ডেসিমাল ওয়ার্নিং (Precision) সমাধান করতে
        builder.Property(b => b.DailyCharge)
               .HasPrecision(18, 2);

        builder.Property(b => b.Notes).HasMaxLength(500);

        // এনাম কনভার্সন (স্ট্রিং হিসেবে সেভ হবে)
        builder.Property(b => b.Status)
               .HasConversion<string>()
               .HasMaxLength(30);

        // ── ৪. রিলেশনশিপ (Shadow Property ফিক্স) ──────────────────────────

        // ক. Ward এর সাথে রিলেশন (Parent)
        builder.HasOne(b => b.Ward)
               .WithMany(w => w.Beds)
               .HasForeignKey(b => b.WardId)
               .OnDelete(DeleteBehavior.Cascade);

        // খ. Admissions এর সাথে রিলেশন (One-to-Many)
        builder.HasMany(b => b.Admissions)
               .WithOne(a => a.Bed)
               .HasForeignKey(a => a.BedId)
               .OnDelete(DeleteBehavior.Restrict);

        // গ. BedBookings এর সাথে রিলেশন
        builder.HasMany(b => b.Bookings)
               .WithOne(bb => bb.Bed)
               .HasForeignKey(bb => bb.BedId)
               .OnDelete(DeleteBehavior.Restrict);

        // ── ৫. ডিফল্ট ভ্যালু ও সেটিংস ───────────────────────────────────────────────────
        builder.Property(b => b.HasOxygen).HasDefaultValue(false);
        builder.Property(b => b.HasMonitor).HasDefaultValue(false);
        builder.Property(b => b.IsIsolation).HasDefaultValue(false);
    }
}