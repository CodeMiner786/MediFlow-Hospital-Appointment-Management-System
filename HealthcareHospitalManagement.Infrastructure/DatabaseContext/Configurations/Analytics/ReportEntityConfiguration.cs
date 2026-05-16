using HealthcareHospitalManagement.Domain.Entities.Analytics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// আপনার প্রোজেক্টের ফোল্ডার স্ট্রাকচার অনুযায়ী সঠিক নেমস্পেস
namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Analytics;

public class ReportEntityConfiguration : IEntityTypeConfiguration<ReportEntity>
{
    public void Configure(EntityTypeBuilder<ReportEntity> builder)
    {
        // ── ১. টেবিল কনফিগারেশন ───────────────────────────────────────────
        // ডুপ্লিকেট ToTable কল রিমুভ করে একবারে কনফিগার করা হয়েছে
        builder.ToTable("ReportEntities", "Finance", t => t.HasComment("সুপার অ্যাডমিন কর্তৃক জেনারেট করা বার্ষিক বা মাসিক রিপোর্টের আর্কাইভ এবং ফাইল লিংক স্টোর করে।"));

        // ── ২. প্রাইমারি কি ─────────────────────────────────────────────────────
        builder.HasKey(r => r.Id);

        // ── ৩. প্রপার্টি কনফিগারেশন ────────────────────────────────────────
        builder.Property(r => r.ReportTitle)
               .IsRequired()
               .HasMaxLength(200)
               .HasComment("রিপোর্টের নাম (যেমন: Annual Financial Report 2026)");

        builder.Property(r => r.GeneratedBy)
               .IsRequired()
               .HasMaxLength(100)
               .HasComment("যে অ্যাডমিন রিপোর্টটি তৈরি করেছেন তার ইউজারনেম বা আইডি");

        // Summary: JSON ডাটা রাখার জন্য (nvarchar(max))
        builder.Property(r => r.Summary)
               .IsRequired(false)
               .HasComment("রিপোর্টের মূল ডাটাগুলোর একটি JSON সামারি (দ্রুত প্রিভিউয়ের জন্য)");

        // ফাইল লিংক কনফিগারেশন
        builder.Property(r => r.PdfUrl)
               .HasMaxLength(500)
               .HasComment("জেনারেট হওয়া PDF ফাইলের পাথ বা URL।");

        builder.Property(r => r.ExcelUrl)
               .HasMaxLength(500)
               .HasComment("জেনারেট হওয়া Excel ফাইলের পাথ বা URL।");

        builder.Property(r => r.WordUrl)
               .HasMaxLength(500)
               .HasComment("জেনারেট হওয়া Word ফাইলের পাথ বা URL।");

        // ── ৪. ইনডেক্স ───────────────────────────────────────────────────────────
        // জেনারেটেড ডেট দিয়ে শর্টিং বা ফিল্টারিং দ্রুত করার জন্য ইনডেক্স ব্যবহার করা হয়েছে
        builder.HasIndex(r => r.GeneratedAt);
        builder.HasIndex(r => r.ReportTitle); // টাইটেল দিয়ে সার্চ করার জন্য
    }
}