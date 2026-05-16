using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Pharmacy
{
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("Prescriptions", "Inventory", t => t.HasComment("ডাক্তার কর্তৃক পেশেন্টকে দেওয়া ওষুধের প্রেসক্রিপশন রেকর্ড।"));

            builder.HasKey(p => p.Id);

            // প্রপার্টি কনফিগারেশন
            builder.Property(p => p.PrescriptionCode).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Diagnosis).HasMaxLength(1000);
            builder.Property(p => p.Notes).HasMaxLength(2000);

            // এনাম কনভার্সন
            builder.Property(p => p.Status)
                   .HasConversion<string>()
                   .HasMaxLength(30);

            // ——— রিলেশনশিপ ———

            // Patient এর সাথে (One-to-Many)
            builder.HasOne(p => p.Patient)
                   .WithMany() // Patient এ কালেকশন না থাকলে এটি খালি থাকবে
                   .HasForeignKey(p => p.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Doctor এর সাথে (One-to-Many)
            builder.HasOne(p => p.Doctor)
                   .WithMany()
                   .HasForeignKey(p => p.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Items এর সাথে (One-to-Many)
            builder.HasMany(p => p.Items)
                   .WithOne(pi => pi.Prescription)
                   .HasForeignKey(pi => pi.PrescriptionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ——— ইনডেক্সিং ———
            builder.HasIndex(p => p.PrescriptionCode).IsUnique();
            builder.HasIndex(p => p.PatientId);
            builder.HasIndex(p => p.DoctorId);
            builder.HasIndex(p => p.PrescribedDate);
        }
    }
}