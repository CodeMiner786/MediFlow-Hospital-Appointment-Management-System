using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctor;

public class DoctorUnavailabilityConfiguration : IEntityTypeConfiguration<DoctorUnavailability>
{
    public void Configure(EntityTypeBuilder<DoctorUnavailability> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("DoctorUnavailabilities", "Finance", t => t.HasComment("ডাক্তারদের সাময়িক অনুপস্থিতি বা ব্রেক রেকর্ড করার টেবিল।"));

        // ── ২. কি এবং ইনডেক্স (Performance) ───────────────────────────────────────────
        builder.HasKey(u => u.Id);

        // নির্দিষ্ট তারিখে ডাক্তারের এভেইল্যাবিলিটি চেক করার জন্য ইনডেক্স
        builder.HasIndex(u => new { u.DoctorId, u.UnavailableDate });

        // ── ৩. কলাম কনফিগারেশন ─────────────────────────────────────────────────────────
        builder.Property(u => u.Reason)
            .HasMaxLength(250)
            .HasComment("অনুপস্থিতির কারণ (উদা: লাঞ্চ ব্রেক, পার্সোনাল কাজ)।");

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────
        builder.HasOne(u => u.Doctor)
               .WithMany(d => d.Unavailabilities)
               .HasForeignKey(u => u.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}