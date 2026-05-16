using HealthcareHospitalManagement.Domain.Entities.Wards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Wards;

public class WardConfiguration : IEntityTypeConfiguration<Ward>
{
    public void Configure(EntityTypeBuilder<Ward> builder)
    {
        // ── ১. টেবিল ও স্কিমা কনফিগারেশন ──────────────────────────────────────────────────
        builder.ToTable("Wards", "Ward", t =>
            t.HasComment("হাসপাতালের বিভিন্ন ওয়ার্ড বা ডিপার্টমেন্টের তথ্য।"));

        // ── ২. কি এবং ইউনিকনেস ────────────────────────────────────────────────────────────
        builder.HasKey(w => w.Id);

        builder.Property(w => w.WardCode)
               .IsRequired()
               .HasMaxLength(20);
        builder.HasIndex(w => w.WardCode).IsUnique();

        builder.Property(w => w.WardName)
               .IsRequired()
               .HasMaxLength(100);
        builder.HasIndex(w => w.WardName).IsUnique();

        // ── ৩. প্রপার্টি ও ভ্যালিডেশন ─────────────────────────────────────────────────────
        builder.Property(w => w.Building).HasMaxLength(100);
        builder.Property(w => w.InchargeNurseName).HasMaxLength(150);
        builder.Property(w => w.Description).HasMaxLength(500);

        // এনাম কনভার্সন (স্ট্রিং হিসেবে সেভ হবে)
        builder.Property(w => w.WardType)
               .HasConversion<string>()
               .HasMaxLength(30);

        // ── ৪. রিলেশনশিপ (One-to-Many with Bed) ──────────────────────────────────────────
        // এটি না থাকলে ডাটাবেজে WardId1 শ্যাডো প্রপার্টি তৈরি হতো
        builder.HasMany(w => w.Beds)
               .WithOne(b => b.Ward)
               .HasForeignKey(b => b.WardId)
               .OnDelete(DeleteBehavior.Cascade); // ওয়ার্ড ডিলিট হলে বেডগুলো ডিলিট হবে

        // ── ৫. ক্যাপাসিটি সেটিংস ও ডিফল্ট ভ্যালু ─────────────────────────────────────────
        builder.Property(w => w.FloorNumber).HasDefaultValue(0);
        builder.Property(w => w.TotalBeds).HasDefaultValue(0);
        builder.Property(w => w.AvailableBeds).HasDefaultValue(0);
    }
}