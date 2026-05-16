using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Dashboard;

public class PharmacyDashboardMetricConfiguration : IEntityTypeConfiguration<PharmacyDashboardMetric>
{
    public void Configure(EntityTypeBuilder<PharmacyDashboardMetric> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("PharmacyDashboardMetrics", "Finance", t => t.HasComment("ফার্মেসিগুলোর দৈনিক অর্ডার, ইনভেন্টরি স্ট্যাটাস এবং আয়ের পরিসংখ্যান টেবিল।"));

        // ── ২. কি এবং ইনডেক্স (Performance Optimization) ──────────────────────────────
        builder.HasKey(m => m.Id);

        // নির্দিষ্ট ফার্মেসির তারিখ অনুযায়ী ডাটা দ্রুত খোঁজার জন্য ইউনিক ইনডেক্স
        builder.HasIndex(m => new { m.PharmacyProfileId, m.MetricDate }).IsUnique();

        // ── ৩. কলাম কনফিগারেশন (Financials & Ratings) ────────────────────────────────
        builder.Property(m => m.TodayRevenue).HasPrecision(18, 2);
        builder.Property(m => m.MonthlyRevenue).HasPrecision(18, 2);
        builder.Property(m => m.PendingPayments).HasPrecision(18, 2);

        builder.Property(m => m.AverageRating).HasPrecision(3, 2);

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────
        builder.HasOne(m => m.PharmacyProfile)
               .WithMany()
               .HasForeignKey(m => m.PharmacyProfileId)
               .OnDelete(DeleteBehavior.Cascade); // ফার্মেসি প্রোফাইল মুছে গেলে ডাটা রাখার দরকার নেই
    }
}