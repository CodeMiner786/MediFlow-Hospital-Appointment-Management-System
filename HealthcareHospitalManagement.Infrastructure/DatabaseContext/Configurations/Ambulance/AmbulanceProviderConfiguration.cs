using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// আপনার ফোল্ডার স্ট্রাকচার অনুযায়ী সঠিক নেমস্পেস
namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Ambulance;

public class AmbulanceProviderConfiguration : IEntityTypeConfiguration<AmbulanceProviderProfile>
{
    public void Configure(EntityTypeBuilder<AmbulanceProviderProfile> builder)
    {
        // ── ১. টেবিল কনফিগারেশন ──────────────────────────────────────────────────
        builder.ToTable("AmbulanceProviderProfiles", "Inventory", t => t.HasComment("অ্যাম্বুলেন্স সেবাদানকারী প্রতিষ্ঠান বা প্রোভাইডারদের প্রোফাইল তথ্য।"));

        // ── ২. প্রাইমারি কি ─────────────────────────────────────────────────────
        builder.HasKey(p => p.Id);

        // ── ৩. প্রপার্টি কনফিগারেশন ────────────────────────────────────────
        builder.Property(p => p.ProviderName)
               .IsRequired()
               .HasMaxLength(150)
               .HasComment("প্রোভাইডার বা কোম্পানির নাম।");

        builder.Property(p => p.RegistrationNumber)
               .IsRequired()
               .HasMaxLength(100)
               .HasComment("সরকারি বা ট্রেড লাইসেন্স রেজিস্ট্রেশন নম্বর।");

        builder.Property(p => p.ContactNumber)
               .IsRequired()
               .HasMaxLength(20)
               .HasComment("অফিসিয়াল যোগাযোগের নম্বর।");

        builder.Property(p => p.EmergencyHotline)
               .IsRequired()
               .HasMaxLength(20)
               .HasComment("জরুরি অ্যাম্বুলেন্স কলের জন্য হটলাইন নম্বর।");

        builder.Property(p => p.Address)
               .IsRequired()
               .HasMaxLength(250)
               .HasComment("প্রোভাইডারের অফিসের পূর্ণ ঠিকানা।");

        builder.Property(p => p.City)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(p => p.District)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(p => p.AverageRating)
               .HasPrecision(3, 2)
               .HasComment("ইউজারদের দেওয়া গড় রেটিং (যেমন: ৪.৫৫)।");

        // ফোরেন কি প্রপার্টিতে কমেন্ট (রিলেশনে নয়)
        builder.Property(p => p.ApplicationUserId)
               .HasComment("সিস্টেম ইউজার অ্যাকাউন্টের সাথে এই প্রোফাইলের লিংক।");

        // ── ৪. রিলেশনশিপ কনফিগারেশন ───────────────────────────────────────────────────

        // ১. Provider to ApplicationUser (One-to-One)
        builder.HasOne(p => p.ApplicationUser)
               .WithOne()
               .HasForeignKey<AmbulanceProviderProfile>(p => p.ApplicationUserId)
               .OnDelete(DeleteBehavior.Restrict);

        // ২. Provider to Wallet (One-to-One)
        builder.HasOne(p => p.Wallet)
               .WithOne(w => w.Provider)
               .HasForeignKey<AmbulanceProviderWallet>(w => w.ProviderId)
               .OnDelete(DeleteBehavior.Cascade);

        // ৩. Provider to Vehicles (One-to-Many)
        builder.HasMany(p => p.Vehicles)
               .WithOne(v => v.Provider)
               .HasForeignKey(v => v.ProviderId)
               .OnDelete(DeleteBehavior.Restrict);

        // ── ৫. ইনডেক্সিং ──────────────────────────────────────────────────
        builder.HasIndex(p => p.RegistrationNumber).IsUnique();
        builder.HasIndex(p => p.EmergencyHotline);
    }
}