using HealthcareHospitalManagement.Domain.Entities.Billing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Billing;

public class BillItemConfiguration : IEntityTypeConfiguration<BillItem>
{
    public void Configure(EntityTypeBuilder<BillItem> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("BillItems", "Finance", t => t.HasComment("বিলের অন্তর্ভুক্ত প্রতিটি সার্ভিস বা আইটেমের বিস্তারিত তালিকা (যেমন: ডক্টর ফি, টেস্ট ফি)।"));

        // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
        builder.HasKey(bi => bi.Id);

        // ── ৩. প্রপার্টি কনফিগারেশন (Validation & Precision) ───────────────────────────

        // সার্ভিস নাম অবশ্যই দিতে হবে এবং সর্বোচ্চ ২০০ ক্যারেক্টার
        builder.Property(bi => bi.ServiceName)
               .IsRequired()
               .HasMaxLength(200);

        // সার্ভিস কোড (ঐচ্ছিক)
        builder.Property(bi => bi.ServiceCode)
               .HasMaxLength(50);

        // ডেসিমাল প্রিসিশন (১৮, ২) - নিখুঁত হিসাবের জন্য
        builder.Property(bi => bi.UnitPrice).HasPrecision(18, 2); // একক মূল্য
        builder.Property(bi => bi.Discount).HasPrecision(18, 2); // আইটেম ভিত্তিক ছাড়
        builder.Property(bi => bi.TotalPrice).HasPrecision(18, 2); // মোট মূল্য (Qty * UnitPrice - Discount)

        // কোয়ান্টিটি (ডিফল্ট ১ থাকতে পারে)
        builder.Property(bi => bi.Quantity)
               .IsRequired();

        // ── ৪. রিলেশনশিপ (Relationships) ──────────────────────────────────────────────

        // Bill to BillItems (One-to-Many)
        // একটি বিল আইটেম অবশ্যই একটি বিলের আন্ডারে থাকবে। 
        // বিল ডিলিট করলে তার সব আইটেম অটোমেটিক ডিলিট হয়ে যাবে (Cascade)।
        builder.HasOne(bi => bi.Bill)
               .WithMany(b => b.Items)
               .HasForeignKey(bi => bi.BillId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}