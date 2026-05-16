using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Pharmacy
{
    public class MedicineStockConfiguration : IEntityTypeConfiguration<MedicineStock>
    {
        public void Configure(EntityTypeBuilder<MedicineStock> builder)
        {
            builder.ToTable("MedicineStocks", "Inventory", t => t.HasComment("ওষুধের ব্যাচভিত্তিক ইনভেন্টরি এবং সাপ্লাই রেকর্ড।"));

            builder.HasKey(ms => ms.Id);

            // ——— প্রপার্টি কনফিগারেশন ———
            builder.Property(ms => ms.BatchNumber).IsRequired().HasMaxLength(100);
            builder.Property(ms => ms.StorageLocation).HasMaxLength(200);

            // ডেসিমাল প্রিসিশন (টাকা-পয়সা)
            builder.Property(ms => ms.PurchasePrice).HasPrecision(18, 2);
            builder.Property(ms => ms.SellingPrice).HasPrecision(18, 2);

            // এনাম কনভার্সন
            builder.Property(ms => ms.StockStatus)
                   .HasConversion<string>()
                   .HasMaxLength(50);

            // ——— রিলেশনশিপ ———

            // Medicine এর সাথে (One-to-Many)
            builder.HasOne(ms => ms.Medicine)
                   .WithMany(m => m.Stocks)
                   .HasForeignKey(ms => ms.MedicineId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Supplier এর সাথে (Optional)
            builder.HasOne(ms => ms.Supplier)
                   .WithMany() // যদি Supplier এনটিটিতে Stocks কালেকশন না থাকে
                   .HasForeignKey(ms => ms.SupplierId)
                   .OnDelete(DeleteBehavior.SetNull);

            // ——— ইনডেক্সিং ———
            builder.HasIndex(ms => ms.BatchNumber);
            builder.HasIndex(ms => ms.ExpiryDate);
            builder.HasIndex(ms => ms.MedicineId);
        }
    }
}