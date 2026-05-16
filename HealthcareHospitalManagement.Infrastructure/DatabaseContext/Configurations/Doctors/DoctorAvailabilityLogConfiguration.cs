using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctors;

public class DoctorAvailabilityLogConfiguration : IEntityTypeConfiguration<DoctorAvailabilityLog>
{
    public void Configure(EntityTypeBuilder<DoctorAvailabilityLog> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("DoctorAvailabilityLogs", "Staff", t => t.HasComment("ডাক্তারদের অনলাইন/অফলাইন বা অন-ডিউটি স্ট্যাটাস পরিবর্তনের ইতিহাস রাখার টেবিল।"));

        // ── ২. কি এবং ইনডেক্স (Performance) ───────────────────────────────────────────
        builder.HasKey(l => l.Id);

        // নির্দিষ্ট ডাক্তারের লেটেস্ট লগ দ্রুত পেতে ইনডেক্স
        builder.HasIndex(l => l.DoctorId);
        builder.HasIndex(l => l.ChangedAt);

        // ── ৩. কলাম কনফিগারেশন ─────────────────────────────────────────────────────────
        builder.Property(l => l.ChangedBy).HasMaxLength(150);
        builder.Property(l => l.Reason).HasMaxLength(500);

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────
        builder.HasOne(l => l.Doctor)
               .WithMany() // ডাক্তার এনটিটি থেকে হয়তো লগের পুরো কালেকশন দরকার নেই (পারফরম্যান্সের জন্য)
               .HasForeignKey(l => l.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}