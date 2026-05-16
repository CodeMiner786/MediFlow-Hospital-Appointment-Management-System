using HealthcareHospitalManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Identity
{
    public class UserLoginHistoryConfiguration : IEntityTypeConfiguration<UserLoginHistory>
    {
        public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("UserLoginHistories", "Identity", t => t.HasComment("ইউজারদের লগইন করার বিস্তারিত ইতিহাস ও ডিভাইস তথ্য।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(lh => lh.Id);


            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────

            builder.Property(lh => lh.FailureReason).HasMaxLength(250);
            builder.Property(lh => lh.DeviceName).HasMaxLength(100);
            builder.Property(lh => lh.DeviceType).HasMaxLength(50);
            builder.Property(lh => lh.Browser).HasMaxLength(100);
            builder.Property(lh => lh.OperatingSystem).HasMaxLength(100);
            builder.Property(lh => lh.UserAgent).HasMaxLength(500);
            builder.Property(lh => lh.IpAddress).HasMaxLength(50);
            builder.Property(lh => lh.Country).HasMaxLength(100);
            builder.Property(lh => lh.City).HasMaxLength(100);

            // এনাম কনভার্সন
            builder.Property(lh => lh.LoginProvider)
                   .HasConversion<string>()
                   .HasMaxLength(50);


            // ── ৪. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            builder.HasOne(lh => lh.User)
                   .WithMany(u => u.LoginHistories)
                   .HasForeignKey(lh => lh.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


            // ── ৫. পারফরম্যান্স (Indexing) ─────────────────────────────────────────────────

            // ইউজার যখন নিজের লগইন হিস্ট্রি দেখবে, তখন UserId এবং LoginAt দিয়ে দ্রুত সার্চ হবে
            builder.HasIndex(lh => new { lh.UserId, lh.LoginAt });

            // আইপি অ্যাড্রেস দিয়ে সার্চ করার জন্য (Security Check)
            builder.HasIndex(lh => lh.IpAddress);
        }
    }
}