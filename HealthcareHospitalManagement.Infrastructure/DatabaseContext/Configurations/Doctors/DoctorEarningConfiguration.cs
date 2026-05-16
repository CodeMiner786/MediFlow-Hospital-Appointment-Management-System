using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctors;

public class DoctorEarningConfiguration : IEntityTypeConfiguration<DoctorEarning>
{
    public void Configure(EntityTypeBuilder<DoctorEarning> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("DoctorEarnings", "Staff", t => t.HasComment("ডাক্তারদের প্রতিটি অ্যাপয়েন্টমেন্ট বা সেশনের আয়ের বিস্তারিত হিসাব এবং হসপিটাল শেয়ার ট্র্যাকিং টেবিল।"));

        // ── ২. কি এবং ইনডেক্স ──────────────────────────────────────────────────────────
        builder.HasKey(e => e.Id);

        // নির্দিষ্ট ডাক্তারের ইনকাম স্টেটমেন্ট দ্রুত জেনারেট করার জন্য ইনডেক্স
        builder.HasIndex(e => e.DoctorId);
        builder.HasIndex(e => e.EarningDate);

        // ── ৩. কলাম কনফিগারেশন (Financial Precision) ──────────────────────────────────
        builder.Property(e => e.TotalFee).HasPrecision(18, 2);
        builder.Property(e => e.HospitalSharePercent).HasPrecision(5, 2); // যেমন: ১০.২৫%
        builder.Property(e => e.HospitalShareAmount).HasPrecision(18, 2);
        builder.Property(e => e.DoctorShareAmount).HasPrecision(18, 2);

        builder.Property(e => e.PaymentReference).HasMaxLength(100);
        builder.Property(e => e.Notes).HasMaxLength(500);

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────
        builder.HasOne(e => e.Doctor)
               .WithMany(d => d.Earnings) // DoctorEntity তে Earnings কালেকশন থাকলে ভালো
               .HasForeignKey(e => e.DoctorId)
               .OnDelete(DeleteBehavior.Restrict); // ইনকাম রেকর্ড ডিলিট করা রিস্কি, তাই Restrict
    }
}