using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Pharmacy
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers", "Inventory", t => t.HasComment("ওষুধ সরবরাহকারী বা ভেন্ডরদের তথ্য।"));

            builder.HasKey(s => s.Id);

            // প্রপার্টি কনফিগারেশন
            builder.Property(s => s.SupplierName).IsRequired().HasMaxLength(200);
            builder.Property(s => s.SupplierCode).IsRequired().HasMaxLength(50);
            builder.Property(s => s.ContactPerson).IsRequired().HasMaxLength(150);
            builder.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(s => s.Email).HasMaxLength(150);
            builder.Property(s => s.Address).IsRequired().HasMaxLength(500);
            builder.Property(s => s.TaxId).HasMaxLength(100);

            // এনাম কনভার্সন
            builder.Property(s => s.Status)
                   .HasConversion<string>()
                   .HasMaxLength(30);

            // ——— রিলেশনশিপ ———

            // MedicineStock এর সাথে (One-to-Many)
            // নোট: MedicineStock কনফিগারেশনে আমরা অলরেডি ফরেন কি সেট করেছি, এখানে কালেকশনটি ডিফাইন করে দিচ্ছি।
            builder.HasMany(s => s.Stocks)
                   .WithOne(ms => ms.Supplier)
                   .HasForeignKey(ms => ms.SupplierId)
                   .OnDelete(DeleteBehavior.SetNull); // সাপ্লায়ার ডিলিট হলেও স্টকের ডেটা থাকবে (হিস্ট্রি রক্ষার জন্য)

            // ——— ইনডেক্সিং ———
            builder.HasIndex(s => s.SupplierCode).IsUnique();
            builder.HasIndex(s => s.SupplierName);
            builder.HasIndex(s => s.PhoneNumber);
        }
    }
}