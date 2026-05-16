using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctors;

public class DoctorDocumentConfiguration : IEntityTypeConfiguration<DoctorDocument>
{
    public void Configure(EntityTypeBuilder<DoctorDocument> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("DoctorDocuments", "Staff", t => t.HasComment("ডাক্তারদের প্রফেশনাল সার্টিফিকেট এবং ভেরিফিকেশন ডকুমেন্টের তালিকা।"));

        // ── ২. কি এবং ইনডেক্স ──────────────────────────────────────────────────────────
        builder.HasKey(d => d.Id);

        // নির্দিষ্ট ডাক্তারের ডকুমেন্ট দ্রুত খোঁজার জন্য ইনডেক্স
        builder.HasIndex(d => d.DoctorId);

        // ── ৩. কলাম কনফিগারেশন ─────────────────────────────────────────────────────────
        builder.Property(d => d.DocumentName).IsRequired().HasMaxLength(200);
        builder.Property(d => d.FileUrl).IsRequired().HasMaxLength(500);
        builder.Property(d => d.FileType).HasMaxLength(50);
        builder.Property(d => d.VerifiedBy).HasMaxLength(150);
        builder.Property(d => d.Notes).HasMaxLength(500);

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────

        // Doctor to Documents (One-to-Many)
        builder.HasOne(d => d.Doctor)
               .WithMany(doc => doc.Documents) // DoctorEntity তে Documents কালেকশন থাকতে হবে
               .HasForeignKey(d => d.DoctorId)
               .OnDelete(DeleteBehavior.Cascade); // ডাক্তার প্রোফাইল ডিলিট হলে ডকুমেন্টও ডিলিট হবে
    }
}