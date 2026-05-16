using HealthcareHospitalManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Identity
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("RolePermissions", "Identity", t => t.HasComment("বিভিন্ন ইউজার রোলের জন্য মডিউল ভিত্তিক পারমিশন সেটিংস।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(rp => rp.Id);


            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────

            builder.Property(rp => rp.Module)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasComment("মডিউলের নাম (যেমন: Billing, Pharmacy, Patient)");

            builder.Property(rp => rp.Description).HasMaxLength(250);

            // এনাম কনভার্সন
            builder.Property(rp => rp.Role)
                   .HasConversion<string>()
                   .HasMaxLength(50);

            builder.Property(rp => rp.Action)
                   .HasConversion<string>()
                   .HasMaxLength(50);


            // ── ৪. পারফরম্যান্স ও ডাটা ইন্টিগ্রিটি ──────────────────────────────────────────

            // Unique Index: একই রোলের জন্য একই মডিউলে একই অ্যাকশন বারবার সেভ হওয়া ঠেকাবে
            builder.HasIndex(rp => new { rp.Role, rp.Module, rp.Action })
                   .IsUnique()
                   .HasDatabaseName("IX_Role_Module_Action_Unique");
        }
    }
}