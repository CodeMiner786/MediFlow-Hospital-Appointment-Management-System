using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Dashboard;

public class AmbulanceDashboardMetricConfiguration : IEntityTypeConfiguration<AmbulanceDashboardMetric>
{
    public void Configure(EntityTypeBuilder<AmbulanceDashboardMetric> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("AmbulanceDashboardMetrics", "Finance", t => t.HasComment("অ্যাম্বুলেন্স প্রোভাইডারদের ব্যক্তিগত ড্যাশবোর্ড পরিসংখ্যানের টেবিল।"));

        // ── ২. কি এবং ইনডেক্স (Performance) ───────────────────────────────────────────
        builder.HasKey(m => m.Id);

        // একই প্রোভাইডারের একই তারিখে যেন ডুপ্লিকেট ম্যাট্রিক না হয়
        builder.HasIndex(m => new { m.ProviderId, m.MetricDate }).IsUnique();

        // ── ৩. কলাম কনফিগারেশন (Financials & Ratings) ────────────────────────────────
        builder.Property(m => m.TodayEarnings).HasPrecision(18, 2);
        builder.Property(m => m.MonthlyEarnings).HasPrecision(18, 2);
        builder.Property(m => m.WalletBalance).HasPrecision(18, 2);
        builder.Property(m => m.PendingPayments).HasPrecision(18, 2);

        // রেটিং সাধারণত ৩.৫ বা ৪.৮ হয়, তাই প্রিসিশন ৩, ২ দেওয়া যায় (মোট ৩ ঘর, দশমিকের পর ২ ঘর)
        builder.Property(m => m.AverageRating).HasPrecision(3, 2);

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────
        builder.HasOne(m => m.Provider)
               .WithMany() // প্রোভাইডারের প্রোফাইলে হয়তো সব ম্যাট্রিক দেখার দরকার নেই
               .HasForeignKey(m => m.ProviderId)
               .OnDelete(DeleteBehavior.Cascade); // প্রোভাইডার মুছে গেলে ড্যাশবোর্ড ডাটার দরকার নেই
    }
}