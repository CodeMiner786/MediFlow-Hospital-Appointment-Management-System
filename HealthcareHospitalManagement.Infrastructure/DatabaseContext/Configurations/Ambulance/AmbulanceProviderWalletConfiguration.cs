using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// আপনার প্রোজেক্টের ফোল্ডার স্ট্রাকচার অনুযায়ী সঠিক নেমস্পেস
namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Ambulance;

public class AmbulanceProviderWalletConfiguration : IEntityTypeConfiguration<AmbulanceProviderWallet>
{
    public void Configure(EntityTypeBuilder<AmbulanceProviderWallet> builder)
    {
        // ── ১. টেবিল কনফিগারেশন ──────────────────────────────────────────────────
        builder.ToTable("AmbulanceProviderWallets", "Inventory", t => t.HasComment("অ্যাম্বুলেন্স প্রোভাইডারদের আয় এবং ব্যালেন্স ট্র্যাকিং ওয়ালেট টেবিল।"));

        // ── ২. প্রাইমারি কি ─────────────────────────────────────────────────────
        builder.HasKey(w => w.Id);

        // ── ৩. প্রপার্টি কনফিগারেশন ────────────────────────────────────────

        // ব্যালেন্সের জন্য প্রিসিশন এবং ডিফল্ট ভ্যালু
        builder.Property(w => w.Balance)
               .HasPrecision(18, 2)
               .HasDefaultValue(0)
               .HasComment("প্রোভাইডারের বর্তমান এভেইলেবল ব্যালেন্স।");

        builder.Property(w => w.TotalEarned)
               .HasPrecision(18, 2)
               .HasDefaultValue(0)
               .HasComment("প্রোভাইডারের এ পর্যন্ত মোট উপার্জিত অর্থ।");

        builder.Property(w => w.TotalWithdrawn)
               .HasPrecision(18, 2)
               .HasDefaultValue(0)
               .HasComment("প্রোভাইডারের এ পর্যন্ত মোট উইথড্র করা অর্থ।");

        // ফোরেন কি প্রপার্টিতে কমেন্ট
        builder.Property(w => w.ProviderId)
               .HasComment("কোন প্রোভাইডারের এই ওয়ালেট তার রেফারেন্স।");

        // ── ৪. রিলেশনশিপ কনফিগারেশন ───────────────────────────────────────────────────

        // ১. Wallet to Provider (One-to-One)
        builder.HasOne(w => w.Provider)
               .WithOne(p => p.Wallet)
               .HasForeignKey<AmbulanceProviderWallet>(w => w.ProviderId)
               .OnDelete(DeleteBehavior.Cascade);

        // ২. Wallet to Transactions (One-to-Many)
        builder.HasMany(w => w.Transactions)
               .WithOne(t => t.Wallet)
               .HasForeignKey(t => t.WalletId)
               .OnDelete(DeleteBehavior.Cascade);

        // ── ৫. ইনডেক্সিং ──────────────────────────────────────────────────
        builder.HasIndex(w => w.ProviderId).IsUnique();
    }
}