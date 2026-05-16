using HealthcareHospitalManagement.Domain.Entities.Wards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Wards;

public class AdmissionConfiguration : IEntityTypeConfiguration<Admission>
{
    public void Configure(EntityTypeBuilder<Admission> builder)
    {
        // ── ১. টেবিল ও স্কিমা কনফিগারেশন ───────────────────────────────────────────────────
        builder.ToTable("Admissions", "Ward", t =>
            t.HasComment("হাসপাতালের ইন-পেশেন্ট বা ওয়ার্ডে ভর্তি হওয়া রোগীদের বিস্তারিত তথ্য।"));

        // ── ২. কি এবং ইন্ডেক্সিং ──────────────────────────────────────────────────────────
        builder.HasKey(a => a.Id);

        builder.Property(a => a.AdmissionCode).IsRequired().HasMaxLength(30);
        builder.HasIndex(a => a.AdmissionCode).IsUnique();

        // ── ৩. প্রপার্টি ভ্যালিডেশন ও সেটিংস ──────────────────────────────────────────────
        builder.Property(a => a.AdmissionReason).IsRequired().HasMaxLength(500);
        builder.Property(a => a.Diagnosis).HasMaxLength(1000);
        builder.Property(a => a.DischargeNotes).HasMaxLength(1000);
        builder.Property(a => a.DischargeSummary).HasMaxLength(2000);
        builder.Property(a => a.TransferReason).HasMaxLength(500);
        builder.Property(a => a.TransferredFrom).HasMaxLength(100);
        builder.Property(a => a.TransferredTo).HasMaxLength(100);

        builder.Property(a => a.Status)
               .HasConversion<string>()
               .HasMaxLength(30);

        builder.Property(a => a.IsTransferred).HasDefaultValue(false);

        // ── ৪. রিলেশনশিপ ───────────────────────────────────────────────────────────────────

        // ✅ FIX 1 (Shadow Property Admission.PatientId1 সমাধান):
        // PatientConfiguration-এ HasMany(p => p.Admissions).WithOne(ad => ad.Patient) আছে।
        // এখানে WithMany(p => p.Admissions) দিয়ে SAME relationship সংজ্ঞায়িত করুন।
        builder.HasOne(a => a.Patient)
               .WithMany(p => p.Admissions)         // ← PatientId1 সমাধান
               .HasForeignKey(a => a.PatientId)
               .OnDelete(DeleteBehavior.Restrict);  // ✅ FIX 3: Multiple cascade path এড়াতে Restrict

        // ✅ Bed রিলেশন: BedEntity তে Admissions collection নেই, তাই WithMany() empty
        builder.HasOne(a => a.Bed)
               .WithMany()
               .HasForeignKey(a => a.BedId)
               .OnDelete(DeleteBehavior.Restrict);

        // ✅ Doctor রিলেশন: DoctorEntity তে Admissions collection নেই
        builder.HasOne(a => a.AdmittingDoctor)
               .WithMany()
               .HasForeignKey(a => a.AdmittingDoctorId)
               .OnDelete(DeleteBehavior.Restrict);

        // ── ৫. ইনডেক্সিং ───────────────────────────────────────────────────────────────────
        builder.HasIndex(a => a.PatientId);
        builder.HasIndex(a => a.Status);
    }
}
