using HealthcareHospitalManagement.Domain.Entities.Appointment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.Configurations.Appointment;

public class AppointmentReminderConfiguration : IEntityTypeConfiguration<AppointmentReminder>
{
    public void Configure(EntityTypeBuilder<AppointmentReminder> builder)
    {
        // ── Table Configuration ───────────────────────────────────────────
        builder.ToTable("AppointmentReminders", "Patient", t => t.HasComment("রোগীদের অ্যাপয়েন্টমেন্টের নোটিফিকেশন বা রিমাইন্ডার পাঠানোর লগ টেবিল।"));

        // ── Primary Key ─────────────────────────────────────────────────────
        builder.HasKey(r => r.Id);

        // ── Properties Configuration ────────────────────────────────────────
        builder.Property(r => r.ErrorMessage)
               .HasMaxLength(500)
               .HasComment("নোটিফিকেশন ফেইল করলে তার কারণ বা এরর মেসেজ।");

        // ── Indexes for Performance ─────────────────────────────────────────
        // ব্যাকগ্রাউন্ড প্রসেস যাতে দ্রুত পেন্ডিং রিমাইন্ডার খুঁজে পায়
        builder.HasIndex(r => new { r.Status, r.ScheduledAt });

        // ── Relationships ───────────────────────────────────────────────────

        // 1. Reminder to Appointment (Many-to-One)
        // একটি অ্যাপয়েন্টমেন্টের জন্য একাধিক রিমাইন্ডার থাকতে পারে (যেমন: ১ দিন আগে ১টি, ১ ঘণ্টা আগে ১টি)।
        builder.HasOne(r => r.Appointment)
               .WithMany() // AppointmentEntity-তে ICollection<AppointmentReminder> থাকলে সেটা এখানে বলা যেত
               .HasForeignKey(r => r.AppointmentId)
               .OnDelete(DeleteBehavior.Cascade);
        // অ্যাপয়েন্টমেন্ট ডিলিট হয়ে গেলে তার রিমাইন্ডার রাখার দরকার নেই।
    }
}