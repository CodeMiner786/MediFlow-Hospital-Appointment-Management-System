using HealthcareHospitalManagement.Domain.Entities.Emergency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Emergency;

public class EmergencyVisitConfiguration : IEntityTypeConfiguration<EmergencyVisit>
{
    public void Configure(EntityTypeBuilder<EmergencyVisit> builder)
    {
        // ── ১. টেবিল ম্যাপিং ──────────────────────────────────────────────────────────────
        builder.ToTable("EmergencyVisits", "Patient", t =>
            t.HasComment("হাসপাতালের ইমারজেন্সি বা জরুরি বিভাগে আসা রোগীদের ভিজিট রেকর্ড।"));

        builder.HasKey(e => e.Id);

        // ── ২. প্রপার্টি কনফিগারেশন ────────────────────────────────────────────────────
        builder.Property(e => e.EmergencyCode)
               .HasMaxLength(50)
               .IsRequired()
               .HasComment("প্রতিটি ইমারজেন্সি ভিজিটের জন্য ইউনিক ট্র্যাকিং কোড।");

        builder.Property(e => e.ChiefComplaint)
               .HasMaxLength(1000)
               .IsRequired()
               .HasComment("রোগীর প্রধান সমস্যা বা অভিযোগের বর্ণনা।");

        builder.Property(e => e.ModeOfArrival)
               .HasMaxLength(100)
               .HasComment("রোগী কীভাবে হাসপাতালে এসেছেন (যেমন: Ambulance, Private Car, Walk-in)।");

        builder.Property(e => e.TriageLevel)
               .HasConversion<string>()
               .HasMaxLength(50)
               .HasComment("রোগীর গুরুত্বের স্তর।");

        builder.Property(e => e.Status)
               .HasConversion<string>()
               .HasMaxLength(50)
               .HasComment("ইমারজেন্সি ভিজিটের বর্তমান অবস্থা।");

        builder.Property(e => e.PatientId).HasComment("সংশ্লিষ্ট রোগীর আইডি।");
        builder.Property(e => e.AttendingDoctorId).HasComment("দায়িত্বপ্রাপ্ত চিকিৎসকের আইডি।");
        builder.Property(e => e.AmbulanceBookingId).HasComment("অ্যাম্বুলেন্স বুকিং রেফারেন্স।");

        // ── ৩. রিলেশনশিপ কনফিগারেশন ──────────────────────────────────────────────────────

        // ✅ FIX 1 (Shadow Property EmergencyVisit.PatientId1 সমাধান):
        // PatientConfiguration-এ HasMany(p => p.EmergencyVisits).WithOne(e => e.Patient) আছে।
        // এখানে WithMany(p => p.EmergencyVisits) দিয়ে SAME relationship refer করতে হবে।
        builder.HasOne(e => e.Patient)
               .WithMany(p => p.EmergencyVisits)    // ← PatientId1 সমাধান
               .HasForeignKey(e => e.PatientId)
               .OnDelete(DeleteBehavior.Restrict);  // ✅ FIX 3: Restrict — cascade path এড়াতে

        // Doctor: DoctorEntity-তে EmergencyVisits collection নেই, তাই WithMany() empty
        builder.HasOne(e => e.AttendingDoctor)
               .WithMany()
               .HasForeignKey(e => e.AttendingDoctorId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);

        // Ambulance Booking (One-to-One): EmergencyVisit সাইডে FK আছে
        builder.HasOne(e => e.AmbulanceBooking)
               .WithOne()
               .HasForeignKey<EmergencyVisit>(e => e.AmbulanceBookingId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);

        // ── ৪. ইনডেক্সিং ──────────────────────────────────────────────────────────────────
        builder.HasIndex(e => e.EmergencyCode).IsUnique();
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.TriageLevel);
        builder.HasIndex(e => new { e.PatientId, e.CreatedAt });
    }
}
