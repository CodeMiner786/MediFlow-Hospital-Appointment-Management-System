using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctor;

public class DoctorScheduleSlotConfiguration : IEntityTypeConfiguration<DoctorScheduleSlot>
{
    public void Configure(EntityTypeBuilder<DoctorScheduleSlot> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("DoctorScheduleSlots", "Finance", t => t.HasComment("ডাক্তারদের জেনারেট করা প্রতিদিনের নির্দিষ্ট অ্যাপয়েন্টমেন্ট স্লটগুলোর টেবিল।"));

        // ── ২. কি এবং ইনডেক্স (Performance Optimization) ──────────────────────────────
        builder.HasKey(s => s.Id);

        // অ্যাপয়েন্টমেন্ট বুকিংয়ের সময় এই ইনডেক্সটি সবথেকে বেশি কাজে লাগবে (Fast Searching)
        builder.HasIndex(s => new { s.DoctorId, s.SlotDate, s.IsBooked, s.IsBlocked })
               .HasDatabaseName("IX_DoctorSlot_Search");

        // ── ৩. কলাম কনফিগারেশন ─────────────────────────────────────────────────────────
        builder.Property(s => s.BlockReason)
            .HasMaxLength(250)
            .HasComment("স্লটটি ব্লক করা হলে তার কারণ (উদা: জরুরি মিটিং)।");

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────

        // Schedule to Slots (One-to-Many)
        builder.HasOne(s => s.DoctorSchedule)
               .WithMany(ds => ds.Slots)
               .HasForeignKey(s => s.DoctorScheduleId)
               .OnDelete(DeleteBehavior.Cascade);

        // Doctor to Slots (One-to-Many)
        builder.HasOne(s => s.Doctor)
               .WithMany() // ডাক্তার এনটিটিতে Slots কালেকশন থাকলে .WithMany(d => d.Slots) দিন
               .HasForeignKey(s => s.DoctorId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}