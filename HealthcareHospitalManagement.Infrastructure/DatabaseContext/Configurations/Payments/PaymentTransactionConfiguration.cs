using HealthcareHospitalManagement.Domain.Entities.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Payments;

public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
{
    public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
        builder.ToTable("PaymentTransactions", "Finance", t => t.HasComment("সিস্টেমের সকল পেমেন্ট এবং লেনদেনের বিস্তারিত তথ্য।"));

        builder.HasKey(pt => pt.Id);

        // স্ট্রিং কনফিগারেশন
        builder.Property(pt => pt.PaymentCode).IsRequired().HasMaxLength(50);
        builder.Property(pt => pt.TransactionId).HasMaxLength(100);
        builder.Property(pt => pt.GatewayTransactionId).HasMaxLength(100);
        builder.Property(pt => pt.ReferenceNumber).HasMaxLength(100);
        builder.Property(pt => pt.ReceivedBy).IsRequired().HasMaxLength(150);
        builder.Property(pt => pt.RefundTransactionId).HasMaxLength(100);
        builder.Property(pt => pt.GatewayResponse).HasColumnType("nvarchar(max)"); // বড় JSON রেসপন্সের জন্য

        // ডেসিমাল প্রিসিশন (টাকা-পয়সার হিসাব)
        builder.Property(pt => pt.Amount).HasPrecision(18, 2);

        // এনাম কনভার্সন
        builder.Property(pt => pt.Status).HasConversion<string>().HasMaxLength(30);
        builder.Property(pt => pt.Destination).HasConversion<string>().HasMaxLength(50);
        builder.Property(pt => pt.PaymentMethod).HasConversion<string>().HasMaxLength(50);
        builder.Property(pt => pt.Gateway).HasConversion<string>().HasMaxLength(50);

        // ——— ইনডেক্সিং (দ্রুত অডিট এবং সার্চের জন্য) ———
        builder.HasIndex(pt => pt.PaymentCode).IsUnique();
        builder.HasIndex(pt => pt.BillId);
        builder.HasIndex(pt => pt.TransactionId);
        builder.HasIndex(pt => pt.PaymentDate);
    }
}