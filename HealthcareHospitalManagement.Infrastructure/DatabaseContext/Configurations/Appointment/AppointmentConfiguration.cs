using HealthcareHospitalManagement.Domain.Entities.Appointment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.Configurations.Appointment;

public class AppointmentConfiguration : IEntityTypeConfiguration<AppointmentEntity>
{
    public void Configure(EntityTypeBuilder<AppointmentEntity> builder)
    {
        // ── Table & Comments ────────────────────────────────────────────────
        builder.ToTable("Appointments", "Patient", t =>
            t.HasComment("রোগী এবং ডাক্তারদের মধ্যকার অ্যাপয়েন্টমেন্ট বা সিরিয়াল ম্যানেজমেন্ট টেবিল।"));

        // ── Primary Key & Unique Index ──────────────────────────────────────
        builder.HasKey(a => a.Id);

        builder.HasIndex(a => a.AppointmentCode).IsUnique();

        // ── Properties Configuration ────────────────────────────────────────
        builder.Property(a => a.AppointmentCode)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(a => a.ReasonForVisit)
               .IsRequired()
               .HasMaxLength(250);

        builder.Property(a => a.Symptoms).HasMaxLength(1000);
        builder.Property(a => a.Notes).HasMaxLength(1000);
        builder.Property(a => a.CancellationReason).HasMaxLength(500);

        builder.Property(a => a.AppointmentType).HasConversion<string>().HasMaxLength(30);
        builder.Property(a => a.Status).HasConversion<string>().HasMaxLength(30);

        // ── Relationships ───────────────────────────────────────────────────

        // ✅ FIX 1 (Shadow Property PatientId1 সমাধান):
        // PatientConfiguration-এ HasMany(p => p.Appointments).WithOne(a => a.Patient) ইতিমধ্যে
        // সংজ্ঞায়িত আছে। এখানে সেই SAME রিলেশন পুনরায় WithMany() (empty) দিয়ে লিখলে
        // EF Core দুটো আলাদা রিলেশন মনে করে এবং PatientId1 shadow property তৈরি করে।
        // সমাধান: এখানে WithMany(p => p.Appointments) স্পষ্টভাবে উল্লেখ করতে হবে।
        builder.HasOne(a => a.Patient)
               .WithMany(p => p.Appointments)       // ← এটাই মূল সমাধান
               .HasForeignKey(a => a.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        // ✅ 2. Appointment to Doctor (Many-to-One)
        builder.HasOne(a => a.Doctor)
               .WithMany(d => d.Appointments)
               .HasForeignKey(a => a.DoctorId)
               .OnDelete(DeleteBehavior.Restrict);

        // ✅ 3. Self-Referencing (Follow-up) — NoAction দিয়ে cascade cycle এড়ানো
        builder.HasOne(a => a.PreviousAppointment)
               .WithMany()
               .HasForeignKey(a => a.PreviousAppointmentId)
               .OnDelete(DeleteBehavior.NoAction);

        // ✅ 4. Appointment to Bill (Many-to-One)
        // Bill এ AppointmentEntity navigation নেই, তাই WithMany() empty রাখা ঠিক আছে
        builder.HasOne(a => a.Bill)
               .WithMany()
               .HasForeignKey(a => a.BillId)
               .OnDelete(DeleteBehavior.SetNull);

        // ── Indexes ─────────────────────────────────────────────────────────
        builder.HasIndex(a => new { a.PatientId, a.AppointmentDate });
        builder.HasIndex(a => new { a.DoctorId, a.AppointmentDate });
        builder.HasIndex(a => a.Status);
    }
}
