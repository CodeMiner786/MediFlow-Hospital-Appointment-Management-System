using HealthcareHospitalManagement.Domain.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class HealthLogConfiguration : IEntityTypeConfiguration<HealthLog>
{
    public void Configure(EntityTypeBuilder<HealthLog> builder)
    {
        // ১. টেবিল ও স্কিমা এবং টেবিল লেভেল কমেন্ট
        builder.ToTable("HealthLogs", "Patients", t => t.HasComment(
            "পেশেন্টদের দৈনন্দিন স্বাস্থ্য সংক্রান্ত লগ (যেমন: ওজন, সুগার, স্টেপস) সংরক্ষণের টেবিল।"
        ));

        // ২. প্রাইমারি কি
        builder.HasKey(h => h.Id);

        // ৩. প্রপার্টি কনফিগারেশন, ডেসিমাল ওয়ার্নিং ফিক্স ও কমেন্ট
        builder.Property(h => h.PatientId)
               .HasComment("লগটি কোন পেশেন্টের, তার ইউনিক আইডি।");

        builder.Property(h => h.LoggedAt)
               .HasComment("স্বাস্থ্য তথ্যটি ঠিক কখন রেকর্ড করা হয়েছে সেই সময়।");

        builder.Property(h => h.WeightKg)
               .HasPrecision(5, 2)
               .HasComment("পেশেন্টের ওজন (Kg), যেমন: ৭০.৫০ কেজি।");

        builder.Property(h => h.HeightCm)
               .HasPrecision(5, 2)
               .HasComment("পেশেন্টের উচ্চতা (Cm), যেমন: ১৭০.২৫ সেমি।");

        builder.Property(h => h.BloodPressureSystolic)
               .HasComment("সিস্টোলিক ব্লাড প্রেসার (উপরের রিডিং), যেমন: ১২০।");

        builder.Property(h => h.BloodPressureDiastolic)
               .HasComment("ডায়াস্টোলিক ব্লাড প্রেসার (নিচের রিডিং), যেমন: ৮০।");

        builder.Property(h => h.BloodGlucose)
               .HasPrecision(5, 2)
               .HasComment("রক্তে শর্করার মাত্রা (Blood Glucose Level)।");

        builder.Property(h => h.HeartRate)
               .HasComment("হার্ট রেট বা হৃদস্পন্দনের গতি (BPM)।");

        builder.Property(h => h.OxygenSaturation)
               .HasPrecision(5, 2)
               .HasComment("রক্তে অক্সিজেনের মাত্রা (SpO2 %)।");

        builder.Property(h => h.TemperatureCelsius)
               .HasPrecision(4, 2)
               .HasComment("শরীরের তাপমাত্রা (Celsius), যেমন: ৯৮.৬০।");

        builder.Property(h => h.StepsCount)
               .HasComment("সারাদিনে মোট কত কদম বা স্টেপস হেঁটেছেন।");

        builder.Property(h => h.SleepHours)
               .HasComment("গত ২৪ ঘণ্টায় ঘুমের সময় (ঘণ্টায়)।");

        builder.Property(h => h.Source)
               .HasMaxLength(50)
               .HasComment("ডাটা সোর্স: Manual, Smartwatch, BPMonitor ইত্যাদি।");

        builder.Property(h => h.DeviceId)
               .HasMaxLength(100)
               .HasComment("যদি কোনো ডিভাইস থেকে ডাটা আসে, তার ইউনিক আইডি।");

        builder.Property(h => h.DeviceModel)
               .HasMaxLength(100)
               .HasComment("ডিভাইসটির মডেলের নাম।");

        builder.Property(h => h.Notes)
               .HasMaxLength(1000)
               .HasComment("লগ সম্পর্কিত অতিরিক্ত কোনো নোট বা মন্তব্য।");

        // ৪. রিলেশনশিপ (Shadow Property Id1 ফিক্স)
        builder.HasOne(h => h.Patient)
               .WithMany(p => p.HealthLogs)
               .HasForeignKey(h => h.PatientId)
               .OnDelete(DeleteBehavior.Cascade);

        // ৫. ইনডেক্সিং (দ্রুত কুয়েরির জন্য)
        builder.HasIndex(h => h.PatientId);
        builder.HasIndex(h => h.LoggedAt);
    }
}
