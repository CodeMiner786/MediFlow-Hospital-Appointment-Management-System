using HealthcareHospitalManagement.Domain.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Patients;

public class PatientReferralConfiguration : IEntityTypeConfiguration<PatientReferral>
{
    public void Configure(EntityTypeBuilder<PatientReferral> builder)
    {
        // ১. টেবিল ও স্কিমা
        builder.ToTable("PatientReferrals", "Patients", t =>
            t.HasComment("পেশেন্টদের এক ডাক্তার বা ডিপার্টমেন্ট থেকে অন্য জায়গায় রেফার করার রেকর্ড।"));

        // ২. প্রাইমারি কি
        builder.HasKey(r => r.Id);

        // ৩. প্রপার্টি কনফিগারেশন
        builder.Property(r => r.ReferralCode)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(r => r.Reason)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(r => r.ClinicalSummary)
               .HasMaxLength(3000);

        builder.Property(r => r.UrgencyLevel)
               .HasMaxLength(20);

        // ৪. রিলেশনশিপ সমাধান (Shadow Property Fix)

        // রেফারকারী ডাক্তার (Referring Doctor) -> ReferralsMade
        builder.HasOne(r => r.ReferringDoctor)
               .WithMany(d => d.ReferralsMade) // এখানে কালেকশন নাম দিন
               .HasForeignKey(r => r.ReferringDoctorId)
               .OnDelete(DeleteBehavior.Restrict);

        // গন্তব্য ডাক্তার (Referred To Doctor) -> ReferralsReceived
        builder.HasOne(r => r.ReferredToDoctor)
               .WithMany(d => d.ReferralsReceived) // এখানে কালেকশন নাম দিন
               .HasForeignKey(r => r.ReferredToDoctorId)
               .OnDelete(DeleteBehavior.Restrict);

        // পেশেন্টের সাথে রিলেশন
        builder.HasOne(r => r.Patient)
               .WithMany()
               .HasForeignKey(r => r.PatientId)
               .OnDelete(DeleteBehavior.Cascade);

        // ৫. ইনডেক্সিং
        builder.HasIndex(r => r.ReferralCode).IsUnique();
        builder.HasIndex(r => r.ReferringDoctorId);
        builder.HasIndex(r => r.PatientId);
    }
}