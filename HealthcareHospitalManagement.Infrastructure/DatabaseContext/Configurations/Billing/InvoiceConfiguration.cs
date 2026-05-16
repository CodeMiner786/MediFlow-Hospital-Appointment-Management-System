using HealthcareHospitalManagement.Domain.Entities.Billing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Billing;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("Invoices", "Finance", t => t.HasComment("বিলের বিপরীতে জেনারেট হওয়া ফাইনাল ইনভয়েস রেকর্ড এবং পিডিএফ ট্র্যাকিং টেবিল।"));

        // ── ২. কি এবং ইনডেক্স ──────────────────────────────────────────────────────────
        builder.HasKey(i => i.Id);

        // ইনভয়েস নাম্বার অবশ্যই ইউনিক হতে হবে
        builder.HasIndex(i => i.InvoiceNumber).IsUnique();

        // ── ৩. কলাম কনফিগারেশন (Validation & Precision) ───────────────────────────
        builder.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(50);

        // ডেসিমাল প্রিসিশন (১৮, ২)
        builder.Property(i => i.TotalAmount).HasPrecision(18, 2);

        // ইউআরএল এবং নোটসের জন্য লেন্থ সেট করা
        builder.Property(i => i.PdfUrl).HasMaxLength(1000);
        builder.Property(i => i.Notes).HasMaxLength(500);

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────

        // Invoice to Patient (Many-to-One)
        builder.HasOne(i => i.Patient)
               .WithMany()
               .HasForeignKey(i => i.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        // Bill to Invoice (One-to-One) রিলেশনটি কিন্তু BillConfiguration এ অলরেডি দেওয়া আছে। 
        // তাই এখানে আলাদা করে আবার দেওয়ার প্রয়োজন নেই, দিলে ডুপ্লিকেট কনফিগারেশন হতে পারে।
    }
}