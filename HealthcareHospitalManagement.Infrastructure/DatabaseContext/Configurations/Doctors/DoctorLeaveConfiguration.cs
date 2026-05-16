using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctors;

public class DoctorLeaveConfiguration : IEntityTypeConfiguration<DoctorLeave>
{
    public void Configure(EntityTypeBuilder<DoctorLeave> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("DoctorLeaves", "Staff", t => t.HasComment("ডাক্তারদের ছুটির আবেদন এবং অনুমোদনের রেকর্ড রাখার টেবিল।"));

        // ── ২. কি এবং ইনডেক্স (Performance Optimization) ──────────────────────────────
        builder.HasKey(l => l.Id);

        // নির্দিষ্ট ডাক্তারের ছুটির ইতিহাস দ্রুত খোঁজার জন্য ইনডেক্স
        builder.HasIndex(l => l.DoctorId);

        // তারিখের রেঞ্জ অনুযায়ী সার্চ করার জন্য ইনডেক্স
        builder.HasIndex(l => new { l.LeaveFrom, l.LeaveTo });

        // ── ৩. কলাম কনফিগারেশন এবং ভ্যালিডেশন ──────────────────────────────────────────
        builder.Property(l => l.Reason)
            .IsRequired()
            .HasMaxLength(500)
            .HasComment("ছুটি নেওয়ার কারণ।");

        builder.Property(l => l.ApprovedBy)
            .HasMaxLength(150)
            .HasComment("যিনি ছুটি অনুমোদন করেছেন (অ্যাডমিন আইডি বা নাম)।");

        builder.Property(l => l.RejectionReason)
            .HasMaxLength(500)
            .HasComment("আবেদন বাতিল হলে তার কারণ।");

        builder.Property(l => l.Notes)
            .HasMaxLength(500)
            .HasComment("অতিরিক্ত কোনো মন্তব্য বা বিশেষ নোট।");

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────
        builder.HasOne(l => l.Doctor)
               .WithMany(d => d.Leaves)
               .HasForeignKey(l => l.DoctorId)
               .OnDelete(DeleteBehavior.Cascade) // ডাক্তার প্রোফাইল মুছে গেলে ছুটির রেকর্ডও মুছে যাবে
               .HasConstraintName("FK_DoctorLeaves_Doctors");
    }
}