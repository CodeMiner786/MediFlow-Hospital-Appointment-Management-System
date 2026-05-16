using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Pharmacy
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.ToTable("Medicines", "Inventory", t => t.HasComment("হাসপাতাল ফার্মেসির ওষুধের তালিকা।"));

            builder.HasKey(m => m.Id);

            // স্ট্রিং কনফিগারেশন
            builder.Property(m => m.MedicineName).IsRequired().HasMaxLength(200);
            builder.Property(m => m.GenericName).IsRequired().HasMaxLength(200);
            builder.Property(m => m.BrandName).IsRequired().HasMaxLength(200);
            builder.Property(m => m.MedicineCode).IsRequired().HasMaxLength(50);
            builder.Property(m => m.Manufacturer).IsRequired().HasMaxLength(200);
            builder.Property(m => m.Strength).IsRequired().HasMaxLength(50);
            builder.Property(m => m.Unit).IsRequired().HasMaxLength(50);

            // প্রাইজ কনফিগারেশন
            builder.Property(m => m.SellingPrice).HasPrecision(18, 2);

            // টেক্সট ফিল্ডস
            builder.Property(m => m.Description).HasMaxLength(1000);
            builder.Property(m => m.SideEffects).HasMaxLength(1000);
            builder.Property(m => m.Contraindications).HasMaxLength(1000);
            builder.Property(m => m.StorageConditions).HasMaxLength(500);

            // এনাম কনভার্সন
            builder.Property(m => m.Category).HasConversion<string>().HasMaxLength(50);

            // ——— রিলেশনশিপ ———

            // PharmacyProfile (1-to-Many)
            builder.HasOne(m => m.PharmacyProfile)
                   .WithMany(p => p.Medicines)
                   .HasForeignKey(m => m.PharmacyProfileId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ——— ইনডেক্সিং (দ্রুত খোঁজার জন্য) ———
            builder.HasIndex(m => m.MedicineCode).IsUnique();
            builder.HasIndex(m => m.MedicineName);
            builder.HasIndex(m => m.GenericName);
        }
    }
}