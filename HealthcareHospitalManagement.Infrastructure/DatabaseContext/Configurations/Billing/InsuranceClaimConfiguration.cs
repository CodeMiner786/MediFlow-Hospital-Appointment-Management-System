using HealthcareHospitalManagement.Domain.Entities.Billing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Billing;

public class InsuranceClaimConfiguration : IEntityTypeConfiguration<InsuranceClaim>
{
    public void Configure(EntityTypeBuilder<InsuranceClaim> builder)
    {
        // ── ১. টেবিল কনফিগারেশন ──────────────────────────────────────────────────────────
        builder.ToTable("InsuranceClaims", "Finance", t =>
            t.HasComment("বিলের বিপরীতে পেশেন্টের ইন্স্যুরেন্স ক্লেইম এবং স্ট্যাটাস ট্র্যাকিং টেবিল।"));

        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.ClaimNumber).IsUnique();

        // ── ২. প্রপার্টি কনফিগারেশন ────────────────────────────────────────────────────
        builder.Property(c => c.ClaimNumber).IsRequired().HasMaxLength(50);
        builder.Property(c => c.InsuranceProvider).IsRequired().HasMaxLength(150);
        builder.Property(c => c.PolicyNumber).IsRequired().HasMaxLength(50);
        builder.Property(c => c.PolicyHolderName).IsRequired().HasMaxLength(150);

        // ✅ FIX 4 (Decimal Precision)
        builder.Property(c => c.ClaimAmount).HasPrecision(18, 2);
        builder.Property(c => c.ApprovedAmount).HasPrecision(18, 2);
        builder.Property(c => c.RejectedAmount).HasPrecision(18, 2);

        builder.Property(c => c.RejectionReason).HasMaxLength(500);
        builder.Property(c => c.DocumentUrls).HasMaxLength(1000);
        builder.Property(c => c.Notes).HasMaxLength(1000);

        builder.Property(c => c.Status).HasConversion<string>().HasMaxLength(30);

        // ── ৩. রিলেশনশিপ (One-to-One with Bill) ──────────────────────────────────────────
        builder.HasOne(c => c.Bill)
               .WithOne(b => b.InsuranceClaim)
               .HasForeignKey<InsuranceClaim>(c => c.BillId)
               .OnDelete(DeleteBehavior.SetNull);

        // ✅ BillId nullable করা হলো
        builder.Property(c => c.BillId)
               .IsRequired(false);
    }
}
