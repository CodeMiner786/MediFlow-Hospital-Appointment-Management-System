using HealthcareHospitalManagement.Domain.Entities.Telemedicine;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Telemedicine;

public class TelemedicineSessionConfiguration : IEntityTypeConfiguration<TelemedicineSession>
{
    public void Configure(EntityTypeBuilder<TelemedicineSession> builder)
    {
        // ── ১. টেবিল কনফিগারেশন ──────────────────────────────────────────────────────────
        builder.ToTable("TelemedicineSessions", "Patient", t =>
            t.HasComment("টেলিকনসালটেশন বা অনলাইন ভিডিও কলের সেশন রেকর্ড।"));

        builder.HasKey(ts => ts.Id);

        // ── ২. প্রপার্টি কনফিগারেশন ────────────────────────────────────────────────────
        builder.Property(ts => ts.SessionCode).IsRequired().HasMaxLength(50);
        builder.HasIndex(ts => ts.SessionCode).IsUnique();

        builder.Property(ts => ts.RoomId).HasMaxLength(100);
        builder.Property(ts => ts.MeetingLink).HasMaxLength(500);
        builder.Property(ts => ts.RecordingUrl).HasMaxLength(500);
        builder.Property(ts => ts.DoctorNotes).HasMaxLength(2000);
        builder.Property(ts => ts.Prescription).HasMaxLength(2000);
        builder.Property(ts => ts.FollowUpInstructions).HasMaxLength(1000);

        builder.Property(ts => ts.Provider).HasConversion<string>().HasMaxLength(30);
        builder.Property(ts => ts.Status).HasConversion<string>().HasMaxLength(30);

        // ── ৩. রিলেশনশিপ কনফিগারেশন ─────────────────────────────────────────────────────

        // Appointment → Session: AppointmentEntity-তে TelemedicineSessions collection নেই
        builder.HasOne(ts => ts.Appointment)
               .WithMany()
               .HasForeignKey(ts => ts.AppointmentId)
               .OnDelete(DeleteBehavior.Restrict);

        // ✅ FIX 1 (Shadow Property PatientId1 সমাধান):
        // TelemedicineSession-এ PatientId FK আছে কিন্তু Patient entity-তে
        // TelemedicineSessions collection নেই।
        // PatientConfiguration-এ এই রিলেশন define করা নেই, তাই এখানে WithMany() empty ঠিক আছে।
        // তবে HasForeignKey স্পষ্ট করা বাধ্যতামূলক।
        builder.HasOne(ts => ts.Patient)
               .WithMany()                           // Patient-এ TelemedicineSessions নেই
               .HasForeignKey(ts => ts.PatientId)
               .OnDelete(DeleteBehavior.Restrict);   // ✅ FIX 3: Restrict — cascade cycle এড়াতে

        // Doctor: DoctorEntity-তে TelemedicineSessions collection নেই
        builder.HasOne(ts => ts.Doctor)
               .WithMany()
               .HasForeignKey(ts => ts.DoctorId)
               .OnDelete(DeleteBehavior.Restrict);

        // ── ৪. ইনডেক্সিং ──────────────────────────────────────────────────────────────────
        builder.HasIndex(ts => ts.ScheduledAt);
        builder.HasIndex(ts => ts.Status);
        builder.HasIndex(ts => new { ts.PatientId, ts.ScheduledAt });
        builder.HasIndex(ts => new { ts.DoctorId, ts.ScheduledAt });
    }
}
