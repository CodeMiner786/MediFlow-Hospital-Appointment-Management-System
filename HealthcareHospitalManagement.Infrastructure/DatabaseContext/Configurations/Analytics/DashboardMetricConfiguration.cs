using HealthcareHospitalManagement.Domain.Entities.Analytics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// আপনার প্রোজেক্টের ফোল্ডার স্ট্রাকচার অনুযায়ী সঠিক নেমস্পেস
namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Analytics;

public class DashboardMetricConfiguration : IEntityTypeConfiguration<DashboardMetric>
{
    public void Configure(EntityTypeBuilder<DashboardMetric> builder)
    {
        // ── ১. টেবিল ও স্কিমা কনফিগারেশন ──────────────────────────────────────────────────
        builder.ToTable("DashboardMetrics", "Finance", t => t.HasComment("হাসপাতালের প্রতিদিনের আয়, রোগী এবং অন্যান্য গুরুত্বপূর্ণ পরিসংখ্যানের ডেটা টেবিল।"));

        // ── ২. প্রাইমারি কি ─────────────────────────────────────────────────────
        builder.HasKey(m => m.Id);

        // ── ৩. ইনডেক্সিং (Unique Index) ────────────────────────────────────────────────────
        // প্রতিদিনের জন্য মাত্র একটি রেকর্ড থাকবে যাতে ডুপ্লিকেট অ্যানালিটিক্স না আসে।
        builder.HasIndex(m => m.MetricDate).IsUnique();

        // ── ৪. রেভিনিউ প্রপার্টিজ (Decimal Precision) ───────────────────────────
        // সব রেভিনিউ ফিল্ডে নিখুঁত হিসাবের জন্য ১৮, ২ এবং বর্ণনা (Comment) যোগ করা হয়েছে।

        builder.Property(m => m.TotalRevenue)
               .HasPrecision(18, 2)
               .HasComment("ঐ দিনের মোট সর্বমোট আয়।");

        builder.Property(m => m.DoctorRevenue)
               .HasPrecision(18, 2)
               .HasComment("ডক্টর অ্যাপয়েন্টমেন্ট থেকে অর্জিত আয়।");

        builder.Property(m => m.PharmacyRevenue)
               .HasPrecision(18, 2)
               .HasComment("ফার্মেসী বা ঔষধ বিক্রি থেকে অর্জিত আয়।");

        builder.Property(m => m.LabRevenue)
               .HasPrecision(18, 2)
               .HasComment("ল্যাব টেস্ট বা প্যাথলজি থেকে অর্জিত আয়।");

        builder.Property(m => m.AmbulanceRevenue)
               .HasPrecision(18, 2)
               .HasComment("অ্যাম্বুলেন্স সার্ভিস থেকে অর্জিত আয়।");

        builder.Property(m => m.BedBookingRevenue)
               .HasPrecision(18, 2)
               .HasComment("বেড বা ওয়ার্ড বুকিং থেকে অর্জিত আয়।");

        builder.Property(m => m.PendingDues)
               .HasPrecision(18, 2)
               .HasComment("ঐ দিনের বকেয়া বা পাওনা টাকার পরিমাণ।");

        // ── ৫. ডিফল্ট ভ্যালু এবং কাউন্টারস ─────────────────────────────────────

        builder.Property(m => m.TotalPatients)
               .HasDefaultValue(0)
               .HasComment("ঐ দিনে মোট সেবা নেওয়া রোগীর সংখ্যা।");

        builder.Property(m => m.TotalAppointments)
               .HasDefaultValue(0)
               .HasComment("ঐ দিনের মোট অ্যাপয়েন্টমেন্ট সংখ্যা।");

        builder.Property(m => m.TotalRegisteredUsers)
               .HasDefaultValue(0)
               .HasComment("সিস্টেমে মোট নিবন্ধিত ইউজারের সংখ্যা।");

        builder.Property(m => m.MetricDate)
               .IsRequired()
               .HasComment("যে তারিখের জন্য এই পরিসংখ্যান তৈরি করা হয়েছে।");
    }
}