using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctor;

public class StaffAttendanceConfiguration : IEntityTypeConfiguration<StaffAttendance>
{
    public void Configure(EntityTypeBuilder<StaffAttendance> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("StaffAttendances", "Finance", t => t.HasComment("হাসপাতালের স্টাফদের প্রতিদিনের উপস্থিতি এবং ছুটির রেকর্ড টেবিল।"));

        builder.HasKey(a => a.Id);

        // ── ২. ইনডেক্সিং (Performance Optimization) ───────────────────────────────────
        // নির্দিষ্ট স্টাফের এবং নির্দিষ্ট তারিখের অ্যাটেনডেন্স দ্রুত পাওয়ার জন্য
        builder.HasIndex(a => new { a.StaffId, a.AttendanceDate });

        // ── ৩. কলাম কনফিগারেশন ─────────────────────────────────────────────────────────
        builder.Property(a => a.LeaveReason)
            .HasMaxLength(250)
            .HasComment("ছুটি নিয়ে থাকলে তার কারণ।");

        builder.Property(a => a.Notes)
            .HasMaxLength(500)
            .HasComment("উপস্থিতি সংক্রান্ত অতিরিক্ত নোট।");

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────
        builder.HasOne(a => a.Staff)
               .WithMany(s => s.Attendances) // StaffEntity-তে ICollection<StaffAttendance> থাকতে হবে
               .HasForeignKey(a => a.StaffId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}