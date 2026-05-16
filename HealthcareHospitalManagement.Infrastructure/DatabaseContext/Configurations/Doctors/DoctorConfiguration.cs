using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctors;

public class DoctorConfiguration : IEntityTypeConfiguration<DoctorEntity>
{
    public void Configure(EntityTypeBuilder<DoctorEntity> builder)
    {
        builder.ToTable("Doctors", "Staff", t => t.HasComment("সিস্টেমের সকল ডাক্তারদের মূল প্রোফাইল এবং প্রফেশনাল ইনফরমেশন টেবিল।"));

        // ── ১. কি এবং ইউনিকনেস ────────────────────────────────────────────────────────
        builder.HasKey(d => d.Id);
        builder.HasIndex(d => d.DoctorCode).IsUnique();
        builder.HasIndex(d => d.LicenseNumber).IsUnique();
        builder.HasIndex(d => d.ApplicationUserId).IsUnique();

        // ── ২. কলাম লেন্থ এবং ভ্যালিডেশন ──────────────────────────────────────────────
        builder.Property(d => d.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(d => d.LastName).IsRequired().HasMaxLength(50);
        builder.Property(d => d.DoctorCode).IsRequired().HasMaxLength(20);
        builder.Property(d => d.LicenseNumber).IsRequired().HasMaxLength(100);
        builder.Property(d => d.Qualifications).IsRequired().HasMaxLength(500);
        builder.Property(d => d.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(d => d.Email).IsRequired().HasMaxLength(150);

        // ── ৩. ফিন্যান্সিয়াল প্রিসিশন ─────────────────────────────────────────────────
        builder.Property(d => d.ConsultationFee).HasPrecision(18, 2);
        builder.Property(d => d.TelemedicineConsultationFee).HasPrecision(18, 2);
        builder.Property(d => d.HomeVisitFee).HasPrecision(18, 2);
        builder.Property(d => d.CustomDoctorSharePercent).HasPrecision(5, 2); // e.g. 85.50%
        builder.Property(d => d.AverageRating).HasPrecision(3, 2);

        // ── ৪. রিলেশনশিপস ──────────────────────────────────────────────────────────────

        // Doctor to Department (Many-to-One)
        builder.HasOne(d => d.Department)
               .WithMany(dept => dept.Doctors)
               .HasForeignKey(d => d.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);

        // Referrals Made (ডাক্তার যখন অন্য কাউকে রেফার করেন)
        builder.HasMany(d => d.ReferralsMade)
               .WithOne(r => r.ReferringDoctor)
               .HasForeignKey(r => r.ReferringDoctorId) // নিশ্চিত করুন এই আইডি আপনার PatientReferral এনটিটিতে আছে
               .OnDelete(DeleteBehavior.Restrict);

        // Referrals Received (ডাক্তারের কাছে যখন কোনো পেশেন্ট রেফার হয়ে আসে)
        builder.HasMany(d => d.ReferralsReceived)
               .WithOne(r => r.ReferredToDoctor) // আপনার এররে সম্ভবত এই নামটা একটু ভিন্ন (TargetDoctor হতে পারে)
               .HasForeignKey(r => r.ReferredToDoctorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(d => d.IsAvailableNow)
               .IsRequired()
               .HasDefaultValue(false)
               .HasComment("ডাক্তার বর্তমানে অ্যাভেইলেবল কিনা তা নির্দেশ করে।");

    }
}