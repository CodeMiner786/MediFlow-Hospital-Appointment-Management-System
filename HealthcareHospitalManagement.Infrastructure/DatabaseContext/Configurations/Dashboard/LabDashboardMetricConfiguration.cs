using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Dashboard;

public class LabDashboardMetricConfiguration : IEntityTypeConfiguration<LabDashboardMetric>
{
    public void Configure(EntityTypeBuilder<LabDashboardMetric> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("LabDashboardMetrics", "Finance", t => t.HasComment("ল্যাব প্রোফাইলগুলোর দৈনিক ড্যাশবোর্ড পরিসংখ্যান এবং আয় ট্র্যাকিং টেবিল।"));

        // ── ২. কি এবং ইনডেক্স (Performance Optimization) ──────────────────────────────
        builder.HasKey(m => m.Id);

        // নির্দিষ্ট ল্যাবের তারিখ অনুযায়ী ডাটা দ্রুত খোঁজার জন্য ইউনিক ইনডেক্স
        builder.HasIndex(m => new { m.LabProfileId, m.MetricDate }).IsUnique();

        // ── ৩. কলাম কনফিগারেশন (Financials & Ratings) ────────────────────────────────
        builder.Property(m => m.TodayRevenue).HasPrecision(18, 2);
        builder.Property(m => m.MonthlyRevenue).HasPrecision(18, 2);
        builder.Property(m => m.PendingPayments).HasPrecision(18, 2);

        builder.Property(m => m.AverageRating).HasPrecision(3, 2);

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────
        builder.HasOne(m => m.LabProfile)
               .WithMany()
               .HasForeignKey(m => m.LabProfileId)
               .OnDelete(DeleteBehavior.Cascade); // ল্যাব প্রোফাইল মুছে গেলে ডাটা রাখার দরকার নেই
    }
}