using HealthcareHospitalManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Identity
{
    public class RoleAssignmentLogConfiguration : IEntityTypeConfiguration<RoleAssignmentLog>
    {
        public void Configure(EntityTypeBuilder<RoleAssignmentLog> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("RoleAssignmentLogs", "Identity", t => t.HasComment("ইউজারদের রোল পরিবর্তনের ইতিহাস ও কারণ ট্র্যাকিং।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(ral => ral.Id);


            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────

            builder.Property(ral => ral.Reason).HasMaxLength(500);
            builder.Property(ral => ral.AssignedByEmail).HasMaxLength(256);
            builder.Property(ral => ral.IpAddress).HasMaxLength(50);
            builder.Property(ral => ral.UserAgent).HasMaxLength(500);

            // এনাম কনভার্সন
            builder.Property(ral => ral.FromRole).HasConversion<string>().HasMaxLength(50);
            builder.Property(ral => ral.ToRole).HasConversion<string>().HasMaxLength(50);
            builder.Property(ral => ral.AssignedByRole).HasConversion<string>().HasMaxLength(50);


            // ── ৪. রিলেশনশিপ কনফিগারেশন (Critical) ──────────────────────────────────────────

            // Target User Relationship
            builder.HasOne(ral => ral.User)
                   .WithMany(u => u.RoleAssignmentLogs)
                   .HasForeignKey(ral => ral.UserId)
                   .OnDelete(DeleteBehavior.Cascade); // ইউজার ডিলিট হলে লগ ডিলিট হবে

            // Admin User (Who assigned) Relationship
            // নোট: এখানে Restrict ব্যবহার করা হয়েছে যাতে অ্যাডমিন ডিলিট করলেও লগটি হারানো না যায়
            builder.HasOne(ral => ral.AssignedByUser)
                   .WithMany()
                   .HasForeignKey(ral => ral.AssignedByUserId)
                   .OnDelete(DeleteBehavior.Restrict);


            // ── ৫. পারফরম্যান্স (Indexing) ─────────────────────────────────────────────────

            builder.HasIndex(ral => ral.UserId);
            builder.HasIndex(ral => ral.AssignedAt);
        }
    }
}