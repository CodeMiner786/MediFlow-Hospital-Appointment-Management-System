using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Pharmacy
{
    public class MedicineOrderItemConfiguration : IEntityTypeConfiguration<MedicineOrderItem>
    {
        public void Configure(EntityTypeBuilder<MedicineOrderItem> builder)
        {
            // ── ১. টেবিল ও কমেন্ট ───────────────────────────────────────────────────────────
            builder.ToTable("MedicineOrderItems", "Inventory", t => t.HasComment("প্রতিটি মেডিসিন অর্ডারের অন্তর্ভুক্ত ওষুধের বিস্তারিত তালিকা।"));

            // ── ২. প্রাইমারি কি ─────────────────────────────────────────────────────────────
            builder.HasKey(oi => oi.Id);

            // ── ৩. প্রপার্টি ও প্রিসিশন (টাকা-পয়সার হিসাব) ───────────────────────────────────
            builder.Property(oi => oi.UnitPrice)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(oi => oi.Discount)
                   .HasPrecision(18, 2)
                   .HasDefaultValue(0);

            builder.Property(oi => oi.TotalPrice)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(oi => oi.Quantity)
                   .IsRequired();

            // ── ৪. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            // MedicineOrder এর সাথে (Many-to-One)
            builder.HasOne(oi => oi.MedicineOrder)
                   .WithMany(o => o.Items)
                   .HasForeignKey(oi => oi.MedicineOrderId)
                   .OnDelete(DeleteBehavior.Cascade); // অর্ডার ডিলিট হলে আইটেমও ডিলিট হবে

            // Medicine এর সাথে (Many-to-One)
            builder.HasOne(oi => oi.Medicine)
                   .WithMany(m => m.OrderItems)
                   .HasForeignKey(oi => oi.MedicineId)
                   .OnDelete(DeleteBehavior.Restrict); // ওষুধ ডিলিট করতে বাধা দিবে যদি কোনো অর্ডারে থাকে

            // ── ৫. ইনডেক্সিং ──────────────────────────────────────────────────────────────
            builder.HasIndex(oi => oi.MedicineOrderId);
            builder.HasIndex(oi => oi.MedicineId);
        }
    }
}