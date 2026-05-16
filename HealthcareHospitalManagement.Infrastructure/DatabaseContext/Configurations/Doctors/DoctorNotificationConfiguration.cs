using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctors;

public class DoctorNotificationConfiguration : IEntityTypeConfiguration<DoctorNotification>
{
    public void Configure(EntityTypeBuilder<DoctorNotification> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("DoctorNotifications", "Staff", t => t.HasComment("ডাক্তারদের জন্য ইন-অ্যাপ নোটিফিকেশন এবং অ্যালার্ট স্টোর করার টেবিল।"));

        // ── ২. কি এবং ইনডেক্স (Performance) ───────────────────────────────────────────
        builder.HasKey(n => n.Id);

        // আনরিড নোটিফিকেশন দ্রুত দেখানোর জন্য ইনডেক্স
        builder.HasIndex(n => new { n.DoctorId, n.IsRead });
        builder.HasIndex(n => n.DeliveredAt);

        // ── ৩. কলাম কনফিগারেশন ─────────────────────────────────────────────────────────
        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(200)
            .HasComment("নোটিফিকেশনের শিরোনাম।");

        builder.Property(n => n.Message)
            .IsRequired()
            .HasMaxLength(1000)
            .HasComment("নোটিফিকেশনের বিস্তারিত বার্তা।");

        builder.Property(n => n.ReferenceType)
            .HasMaxLength(50)
            .HasComment("যে এনটিটির রেফারেন্সে নোটিফিকেশনটি তৈরি হয়েছে।");

        builder.Property(n => n.ActionUrl)
            .HasMaxLength(500)
            .HasComment("নোটিফিকেশনে ক্লিক করলে যে লিংকে রিডাইরেক্ট করবে।");

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────
        builder.HasOne(n => n.Doctor)
               .WithMany() // DoctorEntity-তে নোটিফিকেশন লিস্ট থাকলে .WithMany(d => d.Notifications) দিন
               .HasForeignKey(n => n.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}