using HealthcareHospitalManagement.Domain.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        // ১. টেবিল কনফিগারেশন এবং টেবিল লেভেল কমেন্ট
        builder.ToTable("MedicalRecords", "Patients", t => t.HasComment(
            "পেশেন্টদের ক্লিনিকাল ভিজিট, রোগ নির্ণয় এবং চিকিৎসার বিস্তারিত রেকর্ড।"
        ));

        // ২. কি কনফিগারেশন
        builder.HasKey(m => m.Id);

        // ৩. প্রপার্টি কনফিগারেশন (String Lengths) ও কমেন্ট
        builder.Property(m => m.PatientId)
               .HasComment("রেকর্ডটি কোন পেশেন্টের, তার আইডি।");

        builder.Property(m => m.DoctorId)
               .HasComment("কোন ডাক্তার এই চেকআপটি করেছেন বা রেকর্ডটি তৈরি করেছেন।");

        builder.Property(m => m.VisitDate)
               .HasComment("ডাক্তারের সাথে ভিজিট বা চেকআপের তারিখ ও সময়।");

        builder.Property(m => m.ChiefComplaint)
               .IsRequired()
               .HasMaxLength(1000)
               .HasComment("পেশেন্টের প্রধান সমস্যা বা অভিযোগের বর্ণনা।");

        builder.Property(m => m.Diagnosis)
               .IsRequired()
               .HasMaxLength(2000)
               .HasComment("ডাক্তার কর্তৃক নির্ণীত রোগ বা স্বাস্থ্য সমস্যার নাম।");

        builder.Property(m => m.DifferentialDiagnosis)
               .HasMaxLength(2000)
               .HasComment("সম্ভাব্য অন্যান্য রোগ যেগুলোর লক্ষণ বর্তমান রোগের সাথে মিলে।");

        builder.Property(m => m.TreatmentPlan)
               .HasMaxLength(3000)
               .HasComment("চিকিৎসার পরিকল্পনা বা পরবর্তী পদক্ষেপের বিস্তারিত।");

        builder.Property(m => m.Prescription)
               .HasMaxLength(3000)
               .HasComment("পেশেন্টকে দেওয়া ওষুধের তালিকা বা প্রেসক্রিপশন সামারি।");

        builder.Property(m => m.ClinicalNotes)
               .HasMaxLength(5000)
               .HasComment("ডাক্তারের ব্যক্তিগত ক্লিনিকাল পর্যবেক্ষণ বা অতিরিক্ত নোট।");

        builder.Property(m => m.ReferralTo)
               .HasMaxLength(250)
               .HasComment("যদি পেশেন্টকে অন্য কোনো বিশেষজ্ঞ বা হসপিটালে রেফার করা হয়।");

        builder.Property(m => m.FollowUpDate)
               .HasComment("পরবর্তী ফলো-আপ বা চেকআপের সম্ভাব্য তারিখ।");

        builder.Property(m => m.AttachmentUrls)
               .HasColumnType("nvarchar(max)")
               .HasComment("রিপোর্ট, এক্স-রে বা স্ক্যানের লিংকের তালিকা (JSON ফরম্যাটে)।");

        builder.Property(m => m.IsSharedWithPatient)
               .HasComment("এই রেকর্ডটি পেশেন্ট তার নিজের অ্যাপ থেকে দেখতে পারবে কি না।");

        // ৪. রিলেশনশিপ
        builder.HasOne(m => m.Patient)
               .WithMany(p => p.MedicalRecords)
               .HasForeignKey(m => m.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Doctor)
               .WithMany()
               .HasForeignKey(m => m.DoctorId)
               .OnDelete(DeleteBehavior.Restrict);

        // ৫. ইনডেক্সিং
        builder.HasIndex(m => m.PatientId);
        builder.HasIndex(m => m.DoctorId);
        builder.HasIndex(m => m.VisitDate);
    }
}
