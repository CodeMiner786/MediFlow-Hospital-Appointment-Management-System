using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctors;

public class DoctorScheduleSlotConfiguration : IEntityTypeConfiguration<DoctorScheduleSlot>
{
    // ✅ FIX 5 (Static Readonly)
    private static readonly string TableName = "DoctorScheduleSlots";
    private static readonly string SchemaName = "Staff";

    public void Configure(EntityTypeBuilder<DoctorScheduleSlot> builder)
    {
        // ── ১. টেবিল ও স্কিমা কনফিগারেশন ──────────────────────────────────────────────────
        builder.ToTable(TableName, SchemaName, t =>
            t.HasComment("ডাক্তারদের শিডিউলের অধীনে প্রতিটি নির্দিষ্ট সময়ের স্লট।"));

        // ── ২. কি এবং ইন্ডেক্সিং ──────────────────────────────────────────────────────────
        builder.HasKey(s => s.Id);

        builder.HasIndex(s => new { s.DoctorId, s.SlotDate, s.SlotStartTime }).IsUnique();

        // ── ৩. প্রপার্টি ও ডাটা টাইপ ──────────────────────────────────────────────────────
        builder.Property(s => s.SlotStartTime).IsRequired();
        builder.Property(s => s.SlotEndTime).IsRequired();
        builder.Property(s => s.BlockReason).HasMaxLength(250);

        // ── ৪. রিলেশনশিপ ───────────────────────────────────────────────────────────────────

        // Parent DoctorSchedule → ScheduleSlots (One-to-Many)
        builder.HasOne(s => s.DoctorSchedule)
               .WithMany(ds => ds.Slots)
               .HasForeignKey(s => s.DoctorScheduleId)
               .OnDelete(DeleteBehavior.Cascade);

        // DoctorEntity → ScheduleSlots (Many-to-One)
        // DoctorEntity তে ScheduleSlots collection আছে
        builder.HasOne(s => s.Doctor)
               .WithMany(d => d.ScheduleSlots)
               .HasForeignKey(s => s.DoctorId)
               .OnDelete(DeleteBehavior.Restrict); // ✅ FIX 3: Cascade cycle এড়াতে Restrict

        // ✅ REMOVED WRONG CODE: আগে HasOne<AppointmentEntity>().WithOne()
        // .HasForeignKey<AppointmentEntity>("ScheduleSlotId") ছিল।
        // এটি সম্পূর্ণ ভুল কারণ:
        //   ১. AppointmentEntity-তে ScheduleSlotId property নেই
        //   ২. এটি AppointmentEntity-তে একটি নতুন shadow property তৈরি করে
        //   ৩. AppointmentEntity-র নিজস্ব config আছে — এখানে তা define করা উচিত নয়
        // সমাধান: এই অংশটি সম্পূর্ণ মুছে দেওয়া হয়েছে।

        // ── ৫. ডিফল্ট ভ্যালু ও সেটিংস ─────────────────────────────────────────────────────
        builder.Property(s => s.IsBooked).HasDefaultValue(false);
        builder.Property(s => s.IsBlocked).HasDefaultValue(false);
        builder.Property(s => s.IsTelemedicine).HasDefaultValue(false);
    }
}
