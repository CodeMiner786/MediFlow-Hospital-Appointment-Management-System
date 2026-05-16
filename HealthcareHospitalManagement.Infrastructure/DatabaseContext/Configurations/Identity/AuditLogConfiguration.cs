using HealthcareHospitalManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Identity
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("AuditLogs", "Identity", t => t.HasComment("সিস্টেমের সকল সেনসিটিভ ডাটা পরিবর্তনের রেকর্ড।"));

            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(a => a.Id);

            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────

            builder.Property(a => a.EntityName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.UserEmail).HasMaxLength(256);
            builder.Property(a => a.UserRole).HasMaxLength(50);

            // এনাম কনভার্সন
            builder.Property(a => a.Action)
                   .HasConversion<string>()
                   .HasMaxLength(50);

            // JSON কলামগুলো সাধারণত বড় হয়
            builder.Property(a => a.OldValues).HasColumnType("nvarchar(max)");
            builder.Property(a => a.NewValues).HasColumnType("nvarchar(max)");

            builder.Property(a => a.IpAddress).HasMaxLength(50);
            builder.Property(a => a.UserAgent).HasMaxLength(500);

            // ── ৪. পারফরম্যান্স (Indexing) ─────────────────────────────────────────────────

            // সুপারএডমিন সাধারণত EntityName, UserId বা Timestamp দিয়ে ফিল্টার করবেন
            builder.HasIndex(a => a.EntityName);
            builder.HasIndex(a => a.Timestamp);
            builder.HasIndex(a => a.UserId);
        }
    }
}