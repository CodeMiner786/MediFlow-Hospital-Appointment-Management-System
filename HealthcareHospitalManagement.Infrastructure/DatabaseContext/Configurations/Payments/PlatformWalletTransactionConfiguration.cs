using HealthcareHospitalManagement.Domain.Entities.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Payments
{
    public class PlatformWalletTransactionConfiguration : IEntityTypeConfiguration<PlatformWalletTransaction>
    {
        public void Configure(EntityTypeBuilder<PlatformWalletTransaction> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("PlatformWalletTransactions", "Finance", t => t.HasComment("প্ল্যাটফর্ম ওয়ালেটের প্রতিটি ডেবিট/ক্রেডিট ট্রানজ্যাকশন হিস্ট্রি।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(pwt => pwt.Id);


            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────
            builder.Property(pwt => pwt.TransactionCode)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(pwt => pwt.TransactionType)
                   .IsRequired()
                   .HasMaxLength(50); // "DoctorPayment", "Refund", ইত্যাদি

            builder.Property(pwt => pwt.Description)
                   .HasMaxLength(500);

            // টাকা-পয়সার হিসাবের জন্য decimal(18,2)
            builder.Property(pwt => pwt.Amount)
                   .HasPrecision(18, 2);

            builder.Property(pwt => pwt.BalanceAfter)
                   .HasPrecision(18, 2);


            // ── ৪. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            // PlatformWallet এর সাথে রিলেশন (Many-to-One)
            builder.HasOne(pwt => pwt.Wallet)
                   .WithMany(pw => pw.Transactions)
                   .HasForeignKey(pwt => pwt.WalletId)
                   .OnDelete(DeleteBehavior.Cascade);

            // PaymentTransaction এর সাথে রিলেশন (Optional Many-to-One)
            builder.HasOne(pwt => pwt.Payment)
                   .WithMany(pt => pt.PlatformWalletTransactions)
                   .HasForeignKey(pwt => pwt.PaymentId)
                   .OnDelete(DeleteBehavior.SetNull); // পেমেন্ট ডিলিট হলেও ট্রানজ্যাকশন রেকর্ড থাকবে


            // ── ৫. ইনডেক্সিং (পারফরম্যান্সের জন্য) ──────────────────────────────────────────
            builder.HasIndex(pwt => pwt.TransactionCode).IsUnique();
            builder.HasIndex(pwt => pwt.WalletId);
            builder.HasIndex(pwt => pwt.TransactionAt);
        }
    }
}