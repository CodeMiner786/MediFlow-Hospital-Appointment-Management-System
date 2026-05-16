using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Pharmacy
{
    public class PharmacyServiceListingConfiguration : IEntityTypeConfiguration<PharmacyServiceListing>
    {
        public void Configure(EntityTypeBuilder<PharmacyServiceListing> builder)
        {
            // টেবিল কনফিগারেশন
            builder.ToTable("PharmacyServiceListings", "Inventory", t => t.HasComment("ফার্মেসির অফার বা সার্ভিস যা ফিড-এ প্রদর্শিত হবে।"));

            // প্রাইমারি কি
            builder.HasKey(psl => psl.Id);

            // প্রপার্টি ডিটেইলস
            builder.Property(psl => psl.ServiceTitle)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(psl => psl.Description)
                   .HasMaxLength(1000);

            // প্রাইস কনফিগারেশন (nullable যেহেতু সব সার্ভিসে প্রাইস নাও থাকতে পারে)
            builder.Property(psl => psl.Price)
                   .HasPrecision(18, 2);

            // এনাম কনভার্সন (FeedStatus)
            builder.Property(psl => psl.FeedStatus)
                   .HasConversion<string>()
                   .HasMaxLength(50);

            // ——— রিলেশনশিপ ———

            // PharmacyProfile এর সাথে (Many-to-One)
            builder.HasOne(psl => psl.PharmacyProfile)
                   .WithMany(pp => pp.ServiceListings)
                   .HasForeignKey(psl => psl.PharmacyProfileId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ——— ইনডেক্সিং ———
            builder.HasIndex(psl => psl.PharmacyProfileId);
            builder.HasIndex(psl => psl.FeedStatus);
        }
    }
}