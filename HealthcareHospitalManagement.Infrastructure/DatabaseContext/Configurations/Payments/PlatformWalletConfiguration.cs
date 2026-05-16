using HealthcareHospitalManagement.Domain.Entities.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Payments
{
    public class PlatformWalletConfiguration : IEntityTypeConfiguration<PlatformWallet>
    {
        public void Configure(EntityTypeBuilder<PlatformWallet> builder)
        {
            builder.ToTable("PlatformWallets", "Finance", t => t.HasComment("প্ল্যাটফর্মের কেন্দ্রীয় ওয়ালেট যেখানে সব রেভিনিউ জমা হয়।"));

            builder.HasKey(pw => pw.Id);

            // টাকা-পয়সার হিসাবের জন্য হাই-প্রিসিশন (decimal 18, 2)
            builder.Property(pw => pw.TotalBalance)
                   .HasPrecision(18, 2)
                   .HasDefaultValue(0);

            builder.Property(pw => pw.TotalReceived)
                   .HasPrecision(18, 2)
                   .HasDefaultValue(0);

            builder.Property(pw => pw.TotalRefunded)
                   .HasPrecision(18, 2)
                   .HasDefaultValue(0);

            // ——— রিলেশনশিপ ———

            // এক একটি ওয়ালেটের আন্ডারে অনেক ট্রানজ্যাকশন থাকতে পারে
            builder.HasMany(pw => pw.Transactions)
                   .WithOne(pwt => pwt.Wallet)
                   .HasForeignKey(pwt => pwt.WalletId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}