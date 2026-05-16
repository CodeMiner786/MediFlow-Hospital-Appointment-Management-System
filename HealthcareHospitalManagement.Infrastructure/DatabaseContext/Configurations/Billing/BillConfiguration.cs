using HealthcareHospitalManagement.Domain.Entities.Billing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Billing;

public class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    // ✅ FIX 5 (Static Readonly): ইনডেক্সের জন্য static readonly ব্যবহার
    private static readonly string[] BillIndexes = ["BillDate", "PaymentStatus"];

    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        // ── ১. টেবিল ও স্কিমা কনফিগারেশন ──────────────────────────────────────────────────
        builder.ToTable("Bills", "Finance", t =>
            t.HasComment("রোগীর বিভিন্ন সেবার বিপরীতে তৈরি করা মূল বিল বা ইনভয়েস মাস্টার টেবিল।"));

        // ── ২. প্রাইমারি কি ──────────────────────────────────────────────────────────────
        builder.HasKey(b => b.Id);
        builder.HasIndex(b => b.BillNumber).IsUnique();

        // ── ৩. প্রপার্টি কনফিগারেশন ─────────────────────────────────────────────────────
        builder.Property(b => b.BillNumber)
               .IsRequired()
               .HasMaxLength(50)
               .HasComment("অটো-জেনারেটেড ইউনিক বিল নাম্বার।");

        // ✅ FIX 4 (Decimal Precision): সকল decimal ফিল্ডে precision নির্ধারণ
        builder.Property(b => b.SubTotal).HasPrecision(18, 2);
        builder.Property(b => b.DiscountAmount).HasPrecision(18, 2);
        builder.Property(b => b.TaxAmount).HasPrecision(18, 2);
        builder.Property(b => b.TotalAmount).HasPrecision(18, 2);
        builder.Property(b => b.PaidAmount).HasPrecision(18, 2);
        // DueAmount computed property — database-এ store হয় না, তাই কনফিগ লাগবে না

        builder.Property(b => b.BillType).HasConversion<string>().HasMaxLength(30);
        builder.Property(b => b.PaymentStatus).HasConversion<string>().HasMaxLength(30);

        // ── ৪. রিলেশনশিপ (Relationships) ────────────────────────────────────────────────

        // ✅ FIX 1 (Shadow Property PatientId1 সমাধান):
        // PatientConfiguration-এ HasMany(p => p.Bills).WithOne(b => b.Patient) ইতিমধ্যে আছে।
        // এখানে সেই রিলেশনের Bill-সাইড কনফিগ করতে হবে, এবং WithMany তে navigation স্পষ্ট করতে হবে।
        builder.HasOne(b => b.Patient)
               .WithMany(p => p.Bills)              // ← PatientId1 সমাধান: navigation prop উল্লেখ
               .HasForeignKey(b => b.PatientId)
               .OnDelete(DeleteBehavior.Restrict)   // ✅ FIX 3: Cascade cycle এড়াতে Restrict
               .HasConstraintName("FK_Finance_Bills_PatientId");

        // Bill to Invoice (Many-to-One): Invoice তে Bills collection নেই তাই WithMany() ঠিক আছে
        builder.HasOne(b => b.Invoice)
               .WithMany()
               .HasForeignKey(b => b.InvoiceId)
               .OnDelete(DeleteBehavior.SetNull);

        // Bill to BillItems (One-to-Many): BillItemConfiguration এও define করা আছে — এখানে শুধু একটি দিকে রাখুন
        // উভয় দিক থেকে define করলে duplicate হবে না কারণ EF Core same navigation prop চেনে
        builder.HasMany(b => b.Items)
               .WithOne(bi => bi.Bill)
               .HasForeignKey(bi => bi.BillId)
               .OnDelete(DeleteBehavior.Cascade);

        // Bill to InsuranceClaim (One-to-One): InsuranceClaim সাইডে BillId FK আছে
        builder.HasOne(b => b.InsuranceClaim)
               .WithOne(c => c.Bill)
               .HasForeignKey<InsuranceClaim>(c => c.BillId)
               .OnDelete(DeleteBehavior.SetNull);

        // ── ৫. ইনডেক্সিং (Static Readonly array) ─────────────────────────────────────
        foreach (var col in BillIndexes)
            builder.HasIndex(col);
    }
}
