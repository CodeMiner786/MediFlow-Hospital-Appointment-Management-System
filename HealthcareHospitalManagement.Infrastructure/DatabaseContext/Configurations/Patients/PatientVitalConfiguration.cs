using HealthcareHospitalManagement.Domain.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Patients;

public class PatientVitalConfiguration : IEntityTypeConfiguration<PatientVital>
{
    public void Configure(EntityTypeBuilder<PatientVital> builder)
    {
        // ১. টেবিল কনফিগারেশন ও লেটেস্ট নিয়মে টেবিল কমেন্ট (Obsolete Error Fix)
        builder.ToTable("PatientVitals", "Patients", t =>
            t.HasComment("পেশেন্টদের শারীরিক ভাইটাল প্যারামিটারসমূহ (রক্তচাপ, সুগার ইত্যাদি) সংরক্ষণের টেবিল।"));

        // ২. প্রাইমারি কি
        builder.HasKey(v => v.Id);

        // ৩. প্রপার্টি কনফিগারেশন, ডেসিমাল প্রিসিশন ও কলাম কমেন্ট
        builder.Property(v => v.RecordedAt)
               .HasComment("ভাইটালটি রেকর্ড করার সময়।");

        builder.Property(v => v.RecordedByName)
               .HasMaxLength(100)
               .HasComment("যিনি ভাইটালটি রেকর্ড করেছেন (নার্স বা ডাক্তার)।");

        // ডেসিমাল ওয়ার্নিং ফিক্স (HasPrecision)
        builder.Property(v => v.TemperatureCelsius)
               .HasPrecision(4, 2)
               .HasComment("শরীরের তাপমাত্রা (Celsius), যেমন: ৩৭.৫০।");

        builder.Property(v => v.OxygenSaturation)
               .HasPrecision(5, 2)
               .HasComment("অক্সিজেন লেভেল (SpO2 %)।");

        builder.Property(v => v.WeightKg)
               .HasPrecision(5, 2)
               .HasComment("ওজন (কেজি), যেমন: ৭০.৫০।");

        builder.Property(v => v.HeightCm)
               .HasPrecision(5, 2)
               .HasComment("উচ্চতা (সেমি), যেমন: ১৭৫.০০।");

        builder.Property(v => v.BMI)
               .HasPrecision(4, 2)
               .HasComment("বডি মাস ইনডেক্স (BMI)।");

        builder.Property(v => v.BloodGlucose)
               .HasPrecision(5, 2)
               .HasComment("রক্তে শর্করার পরিমাণ।");

        builder.Property(v => v.Notes)
               .HasMaxLength(500)
               .HasComment("অতিরিক্ত কোনো পর্যবেক্ষণ।");

        // ৪. রিলেশনশিপ (Shadow Property Id1 এরর ফিক্স)
        builder.HasOne(v => v.Patient)
               .WithMany(p => p.Vitals)
               .HasForeignKey(v => v.PatientId)
               .OnDelete(DeleteBehavior.Cascade);

        // ৫. ইনডেক্স
        builder.HasIndex(v => v.PatientId);
        builder.HasIndex(v => v.RecordedAt);
    }
}