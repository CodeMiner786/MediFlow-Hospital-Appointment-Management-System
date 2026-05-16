using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// ফোল্ডার স্ট্রাকচার অনুযায়ী সঠিক নেমস্পেস
namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Ambulance;

public class AmbulanceServiceListingConfiguration : IEntityTypeConfiguration<AmbulanceServiceListing>
{
    public void Configure(EntityTypeBuilder<AmbulanceServiceListing> builder)
    {
        // ── ১. টেবিল ও স্কিমা কনফিগারেশন ──────────────────────────────────────────────────
        // টেবিল নাম: AmbulanceServiceListings, স্কিমা নাম: Inventory
        builder.ToTable("AmbulanceServiceListings", "Inventory", t =>
            t.HasComment("প্রোভাইডারদের অফার করা বিভিন্ন অ্যাম্বুলেন্স সার্ভিসের তালিকা (যেমন: AC, ICU, ফ্রিজার ভ্যান)।"));

        // ── ২. প্রাইমারি কি ─────────────────────────────────────────────────────
        builder.HasKey(s => s.Id);

        // ── ৩. প্রপার্টি কনফিগারেশন ────────────────────────────────────────
        builder.Property(s => s.ServiceTitle)
               .IsRequired()
               .HasMaxLength(150)
               .HasComment("সার্ভিসের নাম বা শিরোনাম।");

        builder.Property(s => s.Description)
               .HasMaxLength(500)
               .HasComment("সার্ভিসের বিস্তারিত বর্ণনা।");

        // মূল্যের জন্য ১৮, ২ প্রিসিশন
        builder.Property(s => s.BasePrice)
               .HasPrecision(18, 2)
               .HasComment("সার্ভিসের ভিত্তি মূল্য বা শুরুর ভাড়া।");

        builder.Property(s => s.ImageUrl)
               .HasMaxLength(255)
               .HasComment("সার্ভিসের ছবি বা আইকনের ইউআরএল।");

        builder.Property(s => s.ProviderId)
               .HasComment("এই সার্ভিসটি কোন প্রোভাইডারের তার রেফারেন্স (Foreign Key)।");

        // ── ৪. রিলেশনশিপ কনফিগারেশন ───────────────────────────────────────────────────

        // ১. Service to Provider (Many-to-One)
        // একজন প্রোভাইডার অনেক ধরণের সার্ভিস লিস্টিং করতে পারেন।
        builder.HasOne(s => s.Provider)
               .WithMany(p => p.ServiceListings)
               .HasForeignKey(s => s.ProviderId)
               .OnDelete(DeleteBehavior.Cascade);

        // ── ৫. ইনডেক্সিং ──────────────────────────────────────────────────
        builder.HasIndex(s => s.ServiceTitle);
    }
}