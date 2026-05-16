using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Dashboard;

public class DoctorDashboardMetricConfiguration : IEntityTypeConfiguration<DoctorDashboardMetric>
{
    public void Configure(EntityTypeBuilder<DoctorDashboardMetric> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("DoctorDashboardMetrics", "Finance", t => t.HasComment("ডাক্তারদের ব্যক্তিগত ড্যাশবোর্ড এবং দৈনিক আয়ের পরিসংখ্যান টেবিল।"));

        // ── ২. কি এবং ইনডেক্স (Performance Optimization) ──────────────────────────────
        builder.HasKey(m => m.Id);

        // নির্দিষ্ট ডাক্তারের তারিখ অনুযায়ী ডাটা দ্রুত খোঁজার জন্য ইউনিক ইনডেক্স
        builder.HasIndex(m => new { m.DoctorId, m.MetricDate }).IsUnique();

        // ── ৩. কলাম কনফিগারেশন (Financials & Ratings) ────────────────────────────────

        // টাকার হিসাবের জন্য স্ট্যান্ডার্ড প্রিসিশন
        builder.Property(m => m.TotalRevenueToday).HasPrecision(18, 2);
        builder.Property(m => m.ConsultationRevenue).HasPrecision(18, 2);
        builder.Property(m => m.TelemedicineRevenue).HasPrecision(18, 2);
        builder.Property(m => m.PendingPayments).HasPrecision(18, 2);
        builder.Property(m => m.MonthlyRevenue).HasPrecision(18, 2);

        // রেটিং সাধারণত ৫.০ এর মধ্যে থাকে (যেমন ৪.৫)
        builder.Property(m => m.AverageRating).HasPrecision(3, 2);

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────
        builder.HasOne(m => m.Doctor)
               .WithMany()
               .HasForeignKey(m => m.DoctorId)
               .OnDelete(DeleteBehavior.Cascade); // ডাক্তার প্রোফাইল ডিলিট হলে ডাটা রাখার দরকার নেই
    }
}