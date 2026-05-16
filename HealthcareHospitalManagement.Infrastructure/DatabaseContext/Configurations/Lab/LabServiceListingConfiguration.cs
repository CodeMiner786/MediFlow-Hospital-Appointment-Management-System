using HealthcareHospitalManagement.Domain.Entities.Lab;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.Configurations.Lab
{
    public class LabServiceListingConfiguration : IEntityTypeConfiguration<LabServiceListing>
    {
        public void Configure(EntityTypeBuilder<LabServiceListing> builder)
        {
            builder.ToTable("LabServiceListings", "Patient", t => t.HasComment("ল্যাবরেটরির অফার করা বিভিন্ন সার্ভিস ও ফিড লিস্টিং।"));

            builder.HasKey(x => x.Id);

            // ——— LabProfile → ServiceListings ———
            builder.HasOne(x => x.LabProfile)
                   .WithMany(p => p.ServiceListings)  // ← এখানে p.ServiceListings specify
                   .HasForeignKey(x => x.LabProfileId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.ServiceTitle)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.Property(x => x.HomeCollectionCharge).HasColumnType("decimal(18,2)");
        }
    }
}