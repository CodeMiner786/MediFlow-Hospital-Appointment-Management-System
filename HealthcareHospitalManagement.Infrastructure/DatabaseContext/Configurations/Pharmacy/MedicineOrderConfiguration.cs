using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Pharmacy
{
    public class MedicineOrderConfiguration : IEntityTypeConfiguration<MedicineOrder>
    {
        public void Configure(EntityTypeBuilder<MedicineOrder> builder)
        {
            builder.ToTable("MedicineOrders", "Inventory",  t => t.HasComment("ফার্মেসি থেকে ওষুধের অর্ডারের মূল রেকর্ড।"));

            builder.HasKey(mo => mo.Id);

            // ——— ১. প্রপার্টি কনফিগারেশন ———
            builder.Property(mo => mo.OrderCode).IsRequired().HasMaxLength(50);
            builder.Property(mo => mo.DeliveryAddress).HasMaxLength(500);
            builder.Property(mo => mo.Notes).HasMaxLength(1000);

            // ফিনান্সিয়াল প্রিসিশন
            builder.Property(mo => mo.TotalAmount).HasPrecision(18, 2);

            // এনাম কনভার্সন
            builder.Property(mo => mo.Status).HasConversion<string>().HasMaxLength(30);


            // ——— ২. রিলেশনশিপ (FIXED) ———

            // Patient এর সাথে রিলেশন
            builder.HasOne(mo => mo.Patient)
                   .WithMany()
                   .HasForeignKey(mo => mo.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Prescription এর সাথে রিলেশন (Optional)
            builder.HasOne(mo => mo.Prescription)
                   .WithMany()
                   .HasForeignKey(mo => mo.PrescriptionId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Items এর সাথে রিলেশন (One-to-Many)
            // ফিক্স: oi.OrderId এর বদলে oi.MedicineOrderId ব্যবহার করা হয়েছে
            builder.HasMany(mo => mo.Items)
                   .WithOne(oi => oi.MedicineOrder)
                   .HasForeignKey(oi => oi.MedicineOrderId)
                   .OnDelete(DeleteBehavior.Cascade);


            // ——— ৩. ইনডেক্সিং ———
            builder.HasIndex(mo => mo.OrderCode).IsUnique();
            builder.HasIndex(mo => mo.PatientId);
            builder.HasIndex(mo => mo.OrderDate);
        }
    }
}