using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctors;

public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
{
    public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
    {
        // ── ১. টেবিল ও স্কিমা কনফিগারেশন ──────────────────────────────────────────────────
        builder.ToTable("DoctorSchedules", "Staff", t =>
            t.HasComment("ডাক্তারদের সাপ্তাহিক ডিউটি শিডিউল এবং কনসালটেশন সেটিংস।"));

        // ── ২. কি এবং ইন্ডেক্সিং ─────────────────────────────────────────────────────────
        builder.HasKey(s => s.Id);

        // একই ডাক্তারের একই দিনে যেন মাল্টিপল শিডিউল কনফ্লিক্ট না করে তার জন্য ইনডেক্স
        builder.HasIndex(s => new { s.DoctorId, s.DayOfWeek, s.ShiftType });

        // ── ৩. প্রপার্টি কনফিগারেশন ও টাইপ কনভার্সন ──────────────────────────────────────

        // TimeOnly ডাটাবেজে স্টোর করার জন্য (SQL Server এ সাধারণত 'time' কলাম হিসেবে থাকে)
        builder.Property(s => s.StartTime).IsRequired();
        builder.Property(s => s.EndTime).IsRequired();

        builder.Property(s => s.Location).HasMaxLength(100);
        builder.Property(s => s.SetBy).IsRequired().HasMaxLength(150);

        // এনাম কনভার্সন
        builder.Property(s => s.ShiftType)
               .HasConversion<string>()
               .HasMaxLength(30);

        builder.Property(s => s.DayOfWeek)
               .HasConversion<string>()
               .HasMaxLength(20);

        // ── ৪. রিলেশনশিপ (শ্যাডো প্রপার্টি রোধে) ──────────────────────────────────────────

        // ক. Doctor এর সাথে রিলেশন
        builder.HasOne(s => s.Doctor)
               .WithMany(d => d.Schedules) // DoctorEntity তে এই কালেকশনটি থাকতে হবে
               .HasForeignKey(s => s.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        // খ. Slots এর সাথে রিলেশন (One-to-Many)
        builder.HasMany(s => s.Slots)
               .WithOne(slot => slot.DoctorSchedule)
               .HasForeignKey(slot => slot.DoctorScheduleId)
               .OnDelete(DeleteBehavior.Cascade);

        // ── ৫. ডিফল্ট ভ্যালু ও ভ্যালিডেশন ───────────────────────────────────────────────
        builder.Property(s => s.SlotDurationMinutes).HasDefaultValue(15);
        builder.Property(s => s.IsAvailable).HasDefaultValue(true);
        builder.Property(s => s.IsTelemedicineSlot).HasDefaultValue(false);
    }
}