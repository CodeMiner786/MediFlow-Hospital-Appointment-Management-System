using HealthcareHospitalManagement.Domain.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Patients;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        // ১. টেবিল ও স্কিমা নির্ধারণ এবং টেবিল লেভেল কমেন্ট
        builder.ToTable("Patients", "Patients", t =>
            t.HasComment("পেশেন্টদের ব্যক্তিগত, কন্টাক্ট এবং মেডিকেল প্রোফাইল সংরক্ষণের মূল টেবিল।"));

        // ২. প্রাইমারি কি
        builder.HasKey(p => p.Id);

        // ৩. প্রপার্টি কনফিগারেশন ও কমেন্ট
        builder.Property(p => p.PatientCode)
               .IsRequired()
               .HasMaxLength(20)
               .HasComment("পেশেন্টের ইউনিক আইডেন্টিফিকেশন কোড।");

        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(p => p.LastName).IsRequired().HasMaxLength(50);

        builder.Property(p => p.NationalId)
               .IsRequired()
               .HasMaxLength(30)
               .HasComment("জাতীয় পরিচয়পত্র বা জন্ম নিবন্ধন নম্বর।");

        // ৪. ডেসিমাল ওয়ার্নিং ফিক্স (HasPrecision)
        builder.Property(p => p.WeightKg)
               .HasPrecision(5, 2)
               .HasComment("পেশেন্টের ওজন (কেজি)।");

        builder.Property(p => p.HeightCm)
               .HasPrecision(5, 2)
               .HasComment("পেশেন্টের উচ্চতা (সেমি)।");

        builder.Property(p => p.BMI)
               .HasPrecision(4, 2)
               .HasComment("বডি মাস ইনডেক্স।");

        // ৫. কন্টাক্ট ও অ্যাড্রেস
        builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(p => p.Email).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Address).HasMaxLength(500);

        // ৬. মেডিকেল ও ইনস্যুরেন্স ইনফো
        builder.Property(p => p.Allergies).HasMaxLength(1000);
        builder.Property(p => p.ChronicDiseases).HasMaxLength(1000);
        builder.Property(p => p.InsuranceProvider).HasMaxLength(100);

        // ৭. রিলেশনশিপ ও নেভিগেশন (Shadow Property 'Id1' ফিক্স করতে)

        // Vitals এর সাথে ১-টু-মেনি
        builder.HasMany(p => p.Vitals)
               .WithOne(v => v.Patient)
               .HasForeignKey(v => v.PatientId)
               .OnDelete(DeleteBehavior.Cascade);

        // HealthLogs এর সাথে ১-টু-মেনি
        builder.HasMany(p => p.HealthLogs)
               .WithOne(h => h.Patient)
               .HasForeignKey(h => h.PatientId)
               .OnDelete(DeleteBehavior.Cascade);

        // MedicalRecords এর সাথে ১-টু-মেনি
        builder.HasMany(p => p.MedicalRecords)
               .WithOne(m => m.Patient)
               .HasForeignKey(m => m.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        // অন্যান্য মডিউলের সাথে রিলেশন (এগুলো নিশ্চিত করবে যেন Id1 জেনারেট না হয়)
        builder.HasMany(p => p.Appointments)
               .WithOne(a => a.Patient)
               .HasForeignKey(a => a.PatientId);

        builder.HasMany(p => p.Bills)
               .WithOne(b => b.Patient)
               .HasForeignKey(b => b.PatientId);

        builder.HasMany(p => p.LabOrders)
               .WithOne(l => l.Patient)
               .HasForeignKey(l => l.PatientId);

        // ৮. ইনডেক্সিং (দ্রুত সার্চের জন্য)
        builder.HasIndex(p => p.PatientCode).IsUnique();
        builder.HasIndex(p => p.Email).IsUnique();
        builder.HasIndex(p => p.NationalId).IsUnique();
        builder.HasIndex(p => p.PhoneNumber);
    }
}