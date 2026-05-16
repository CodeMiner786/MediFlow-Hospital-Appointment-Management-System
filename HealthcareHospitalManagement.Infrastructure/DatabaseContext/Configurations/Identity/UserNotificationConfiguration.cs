using HealthcareHospitalManagement.Domain.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Identity
{
    public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("UserNotifications", "Identity", t => t.HasComment("ইউজারদের জন্য ইন-অ্যাপ, ইমেইল এবং এসএমএস নোটিফিকেশন রেকর্ড।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(n => n.Id);


            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────

            builder.Property(n => n.Title).IsRequired().HasMaxLength(200);
            builder.Property(n => n.TitleBn).HasMaxLength(300);
            builder.Property(n => n.Message).IsRequired().HasMaxLength(1000);
            builder.Property(n => n.MessageBn).HasMaxLength(1500);

            builder.Property(n => n.ActionUrl).HasMaxLength(500);
            builder.Property(n => n.ReferenceType).HasMaxLength(100);
            builder.Property(n => n.IconType).HasMaxLength(50);

            // এনাম কনভার্সন
            builder.Property(n => n.Category).HasConversion<string>().HasMaxLength(50);
            builder.Property(n => n.Priority).HasConversion<string>().HasMaxLength(30);


            // ── ৪. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            builder.HasOne(n => n.User)
                   .WithMany(u => u.Notifications)
                   .HasForeignKey(n => n.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


            // ── ৫. পারফরম্যান্স (Indexing) ─────────────────────────────────────────────────

            // ইউজার যখন নোটিফিকেশন লিস্ট দেখবে (সবচেয়ে কমন কুয়েরি)
            builder.HasIndex(n => new { n.UserId, n.IsRead, n.CreatedAt });

            // রেফারেন্স আইডি দিয়ে দ্রুত সার্চ করার জন্য
            builder.HasIndex(n => n.ReferenceId);
        }
    }
}