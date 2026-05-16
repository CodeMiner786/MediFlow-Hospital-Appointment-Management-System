using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Pharmacy
{
    public class PharmacyProfileConfiguration : IEntityTypeConfiguration<PharmacyProfile>
    {
        public void Configure(EntityTypeBuilder<PharmacyProfile> builder)
        {
            // টেবিল নেম এবং কমেন্ট
            builder.ToTable("PharmacyProfiles", "Inventory", t => t.HasComment("ফার্মেসির প্রোফাইল এবং লাইসেন্স সংক্রান্ত তথ্য।"));

            // প্রাইমারি কি
            builder.HasKey(pp => pp.Id);

            // স্ট্রিং কনফিগারেশন
            builder.Property(pp => pp.PharmacyName).IsRequired().HasMaxLength(200);
            builder.Property(pp => pp.LicenseNumber).IsRequired().HasMaxLength(100);
            builder.Property(pp => pp.ContactNumber).IsRequired().HasMaxLength(20);
            builder.Property(pp => pp.Address).IsRequired().HasMaxLength(500);
            builder.Property(pp => pp.City).IsRequired().HasMaxLength(100);

            // ডেসিমাল প্রিসিশন (রেটিং এর জন্য)
            builder.Property(pp => pp.AverageRating).HasPrecision(3, 2);

            // ——— রিলেশনশিপ ———

            // ApplicationUser এর সাথে (One-to-One)
            builder.HasOne(pp => pp.ApplicationUser)
                   .WithOne() // Identity সাইড থেকে প্রোফাইল এক্সেস করার জন্য
                   .HasForeignKey<PharmacyProfile>(pp => pp.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Medicines এর সাথে (One-to-Many)
            builder.HasMany(pp => pp.Medicines)
                   .WithOne(m => m.PharmacyProfile)
                   .HasForeignKey(m => m.PharmacyProfileId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ServiceListings এর সাথে (One-to-Many)
            builder.HasMany(pp => pp.ServiceListings)
                   .WithOne(ps => ps.PharmacyProfile)
                   .HasForeignKey(ps => ps.PharmacyProfileId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ——— ইনডেক্সিং ———
            builder.HasIndex(pp => pp.LicenseNumber).IsUnique();
            builder.HasIndex(pp => pp.ApplicationUserId).IsUnique();
            builder.HasIndex(pp => pp.PharmacyName);
        }
    }
}