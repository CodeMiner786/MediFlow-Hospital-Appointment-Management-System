using HealthcareHospitalManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Identity
{
    public class UserPermissionOverrideConfiguration : IEntityTypeConfiguration<UserPermissionOverride>
    {
        public void Configure(EntityTypeBuilder<UserPermissionOverride> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("UserPermissionOverrides", "Identity", t => t.HasComment("ইউজারের জন্য রোলের বাইরে আলাদাভাবে পারমিশন সেট করার রেকর্ড।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(upo => upo.Id);


            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────

            builder.Property(upo => upo.Module)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasComment("মডিউলের নাম (যেমন: Billing, Inventory)");

            builder.Property(upo => upo.GrantedBy).HasMaxLength(256);
            builder.Property(upo => upo.Reason).HasMaxLength(500);
            builder.Property(upo => upo.IpAddress).HasMaxLength(50);
            builder.Property(upo => upo.UserAgent).HasMaxLength(500);

            // এনাম কনভার্সন
            builder.Property(upo => upo.Action)
                   .HasConversion<string>()
                   .HasMaxLength(50);


            // ── ৪. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            builder.HasOne(upo => upo.User)
                   .WithMany(u => u.PermissionOverrides)
                   .HasForeignKey(upo => upo.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


            // ── ৫. পারফরম্যান্স ও ডাটা ইন্টিগ্রিটি ──────────────────────────────────────────

            // একই ইউজারের জন্য একই মডিউলে একই অ্যাকশন বারবার ওভাররাইড হওয়া ঠেকাবে
            builder.HasIndex(upo => new { upo.UserId, upo.Module, upo.Action })
                   .IsUnique()
                   .HasDatabaseName("IX_User_Module_Action_Override_Unique");
        }
    }
}