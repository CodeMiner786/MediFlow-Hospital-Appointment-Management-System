using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Pharmacy
{
    public class PrescriptionItemConfiguration : IEntityTypeConfiguration<PrescriptionItem>
    {
        public void Configure(EntityTypeBuilder<PrescriptionItem> builder)
        {
            builder.ToTable("PrescriptionItems", "Inventory", t => t.HasComment("প্রেসক্রিপশনে থাকা নির্দিষ্ট ওষুধের ডোজ এবং নিয়মাবলী।"));

            builder.HasKey(pi => pi.Id);

            // স্ট্রিং কনফিগারেশন
            builder.Property(pi => pi.Dosage).IsRequired().HasMaxLength(100);
            builder.Property(pi => pi.Frequency).IsRequired().HasMaxLength(100);
            builder.Property(pi => pi.Route).HasMaxLength(50);
            builder.Property(pi => pi.SpecialInstructions).HasMaxLength(1000);

            // ——— রিলেশনশিপ ———

            // Prescription এর সাথে (Many-to-One)
            builder.HasOne(pi => pi.Prescription)
                   .WithMany(p => p.Items)
                   .HasForeignKey(pi => pi.PrescriptionId)
                   .OnDelete(DeleteBehavior.Cascade); // প্রেসক্রিপশন ডিলিট হলে আইটেমও ডিলিট হবে

            // Medicine এর সাথে (Many-to-One)
            builder.HasOne(pi => pi.Medicine)
                   .WithMany(m => m.PrescriptionItems)
                   .HasForeignKey(pi => pi.MedicineId)
                   .OnDelete(DeleteBehavior.Restrict); // ওষুধ ডিলিট করতে বাধা দেবে যদি এটি কোনো প্রেসক্রিপশনে থাকে

            // ——— ইনডেক্সিং ———
            builder.HasIndex(pi => pi.PrescriptionId);
            builder.HasIndex(pi => pi.MedicineId);
        }
    }
}