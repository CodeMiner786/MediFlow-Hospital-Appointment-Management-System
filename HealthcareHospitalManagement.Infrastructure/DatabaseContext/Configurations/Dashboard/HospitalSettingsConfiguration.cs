using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctor;

public class HospitalSettingsConfiguration : IEntityTypeConfiguration<HospitalSettings>
{
    public void Configure(EntityTypeBuilder<HospitalSettings> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("HospitalSettings", "Finance", t => t.HasComment("হাসপাতালের গ্লোবাল কনফিগারেশন এবং রেভিনিউ শেয়ারিং পার্সেন্টেজ টেবিল।"));

        builder.HasKey(s => s.Id);

        // ── ২. কলাম কনফিগারেশন (Precision) ─────────────────────────────────────────────
        // পার্সেন্টেজের জন্য ৫টি সংখ্যা এবং দশমিকের পরে ২টি ঘর (যেমন: ৯৯.৯৯%)
        builder.Property(s => s.DefaultPlatformSharePercent)
            .HasPrecision(5, 2)
            .HasComment("হাসপাতালের ডিফল্ট কমিশন শতাংশ।");

        builder.Property(s => s.DefaultDoctorSharePercent)
            .HasPrecision(5, 2)
            .HasComment("ডাক্তারের ডিফল্ট আয়ের শতাংশ।");
    }
}