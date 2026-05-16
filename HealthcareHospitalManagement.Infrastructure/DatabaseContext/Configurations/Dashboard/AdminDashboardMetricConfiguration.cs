using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Dashboard;

public class AdminDashboardMetricConfiguration : IEntityTypeConfiguration<AdminDashboardMetric>
{
    public void Configure(EntityTypeBuilder<AdminDashboardMetric> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("AdminDashboardMetrics", "Finance", t => t.HasComment("সুপার অ্যাডমিন ড্যাশবোর্ডের জন্য প্রতিদিনের পরিসংখ্যানের স্ন্যাপশট।"));

        // ── ২. কি এবং ইনডেক্স ──────────────────────────────────────────────────────────
        builder.HasKey(m => m.Id);

        // তারিখ অনুযায়ী ডাটা দ্রুত খোঁজার জন্য ইনডেক্স
        builder.HasIndex(m => m.MetricDate).IsUnique();

        // ── ৩. কলাম কনফিগারেশন (Validation & Precision) ──────────────────────────────

        // টাকা বা রেভিনিউ সম্পর্কিত সব ডেসিমাল প্রিসিশন (১৮, ২)
        builder.Property(m => m.PlatformRevenueToday).HasPrecision(18, 2);
        builder.Property(m => m.DoctorRevenueToday).HasPrecision(18, 2);
        builder.Property(m => m.LabRevenueToday).HasPrecision(18, 2);
        builder.Property(m => m.PharmacyRevenueToday).HasPrecision(18, 2);
        builder.Property(m => m.BedBookingRevenueToday).HasPrecision(18, 2);
        builder.Property(m => m.MonthlyPlatformRevenue).HasPrecision(18, 2);
        builder.Property(m => m.TotalAmbulanceWalletBalance).HasPrecision(18, 2);

        // রিপোর্ট মেটাডাটা লেন্থ
        builder.Property(m => m.LastReportGeneratedAt).HasMaxLength(100);
    }
}