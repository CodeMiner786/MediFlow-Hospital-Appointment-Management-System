using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctors;

public class DoctorPerformanceReportConfiguration : IEntityTypeConfiguration<DoctorPerformanceReport>
{
    // ✅ FIX 5 (Static Readonly): C# 12 warning — inline array argument-এর বদলে static readonly ব্যবহার
    private static readonly string[] ReportDateRange = ["FromDate", "ToDate"];

    public void Configure(EntityTypeBuilder<DoctorPerformanceReport> builder)
    {
        // ── ১. টেবিল ও স্কিমা ────────────────────────────────────────────────────────────
        builder.ToTable("DoctorPerformanceReports", "Staff", t =>
            t.HasComment("ডাক্তারদের পারফরম্যান্স এবং রেভিনিউ সংক্রান্ত বিস্তারিত রিপোর্ট টেবিল।"));

        // ── ২. কি এবং ইনডেক্স ─────────────────────────────────────────────────────────────
        builder.HasKey(r => r.Id);
        builder.HasIndex(r => r.DoctorId);

        // ✅ FIX 5: static readonly array দিয়ে composite index
        builder.HasIndex(ReportDateRange);

        // ── ৩. ডেসিমাল প্রিসিশন ─────────────────────────────────────────────────────────
        // ✅ FIX 4 (Decimal Precision সমাধান): সকল decimal ফিল্ডে HasPrecision নির্ধারণ
        // এটি না করলে EF Core warning দেয় এবং SQL Server-এ precision ভুল হতে পারে।

        // হার ও রেটিং (percentage/ratio fields)
        builder.Property(r => r.CompletionRate).HasPrecision(5, 2);          // যেমন: 99.99%
        builder.Property(r => r.AverageConsultationMinutes).HasPrecision(10, 2);
        builder.Property(r => r.AverageRating).HasPrecision(3, 2);           // যেমন: 4.95

        // ✅ ফিন্যান্সিয়াল ডাটা (Money Fields — precision 18, scale 2 industry standard)
        builder.Property(r => r.TotalRevenue).HasPrecision(18, 2);
        builder.Property(r => r.ConsultationRevenue).HasPrecision(18, 2);
        builder.Property(r => r.TelemedicineRevenue).HasPrecision(18, 2);
        builder.Property(r => r.PlatformShareAmount).HasPrecision(18, 2);
        builder.Property(r => r.DoctorShareAmount).HasPrecision(18, 2);

        // ── ৪. স্ট্রিং লেন্থ ও এনাম ────────────────────────────────────────────────────
        builder.Property(r => r.GeneratedBy).HasMaxLength(150);
        builder.Property(r => r.Department).HasMaxLength(100);
        builder.Property(r => r.PdfUrl).HasMaxLength(500);

        builder.Property(r => r.PeriodType).HasConversion<string>().HasMaxLength(30);
        builder.Property(r => r.Status).HasConversion<string>().HasMaxLength(30);

        // ── ৫. রিলেশনশিপ ──────────────────────────────────────────────────────────────────
        // ✅ FIX 1: DoctorEntity তে PerformanceReports ICollection আছে, তাই WithMany(d => d.PerformanceReports)
        builder.HasOne(r => r.Doctor)
               .WithMany(d => d.PerformanceReports)
               .HasForeignKey(r => r.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
