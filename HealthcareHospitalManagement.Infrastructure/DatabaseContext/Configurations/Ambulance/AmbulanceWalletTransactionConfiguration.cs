using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// ফোল্ডার স্ট্রাকচার অনুযায়ী সঠিক নেমস্পেস
namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Ambulance;

public class AmbulanceWalletTransactionConfiguration : IEntityTypeConfiguration<AmbulanceWalletTransaction>
{
    public void Configure(EntityTypeBuilder<AmbulanceWalletTransaction> builder)
    {
        // ── ১. টেবিল ও স্কিমা কনফিগারেশন ──────────────────────────────────────────────────
        builder.ToTable("AmbulanceWalletTransactions", "Inventory", t => t.HasComment("অ্যাম্বুলেন্স প্রোভাইডারদের ওয়ালেটের প্রতিটি লেনদেনের (আয়/উইথড্র) বিস্তারিত রেকর্ড।"));

        // ── ২. প্রাইমারি কি ─────────────────────────────────────────────────────
        builder.HasKey(t => t.Id);

        // ── ৩. ইনডেক্সিং (Unique Index) ────────────────────────────────────────────────────
        // প্রতিটি ট্রানজেকশন কোড অবশ্যই ইউনিক হতে হবে যাতে ডুপ্লিকেট পেমেন্ট না হয়।
        builder.HasIndex(t => t.TransactionCode).IsUnique();

        // ── ৪. প্রপার্টি কনফিগারেশন ────────────────────────────────────────
        builder.Property(t => t.TransactionCode)
               .IsRequired()
               .HasMaxLength(50)
               .HasComment("ইউনিক ট্রানজেকশন আইডি বা রেফারেন্স কোড।");

        builder.Property(t => t.Description)
               .HasMaxLength(250)
               .HasComment("লেনদেনের সংক্ষিপ্ত বিবরণ।");

        // টাকার অংকের জন্য নিখুঁত প্রিসিশন (১৮, ২)
        builder.Property(t => t.Amount)
               .HasPrecision(18, 2)
               .HasComment("লেনদেনের পরিমাণ (টাকা)।");

        builder.Property(t => t.BalanceAfter)
               .HasPrecision(18, 2)
               .HasComment("এই লেনদেনটি হওয়ার পর ওয়ালেটের মোট ব্যালেন্স।");

        // ফোরেন কি প্রপার্টিতে কমেন্ট
        builder.Property(t => t.WalletId)
               .HasComment("কোন ওয়ালেটের আন্ডারে এই ট্রানজেকশনটি হয়েছে।");

        // ── ৫. রিলেশনশিপ কনফিগারেশন ───────────────────────────────────────────────────

        // ১. Transaction to Wallet (Many-to-One)
        builder.HasOne(t => t.Wallet)
               .WithMany(w => w.Transactions)
               .HasForeignKey(t => t.WalletId)
               .OnDelete(DeleteBehavior.Cascade);

        // ২. Transaction to Booking (Many-to-One, Optional)
        builder.HasOne(t => t.Booking)
               .WithMany()
               .HasForeignKey(t => t.BookingId)
               .OnDelete(DeleteBehavior.SetNull);
        // বুকিং ডিলিট হয়ে গেলেও পেমেন্ট হিস্টোরি রিপোর্টে থাকবে (NULL হিসেবে)।
    }
}