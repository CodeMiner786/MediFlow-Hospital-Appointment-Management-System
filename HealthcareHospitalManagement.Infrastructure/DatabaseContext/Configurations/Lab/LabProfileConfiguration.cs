using HealthcareHospitalManagement.Domain.Entities.Lab;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.Configurations.Lab
{
    public class LabProfileConfiguration : IEntityTypeConfiguration<LabProfile>
    {
        public void Configure(EntityTypeBuilder<LabProfile> builder)
        {
            builder.ToTable("LabProfiles", "Patient", t => t.HasComment("ল্যাব ডিপার্টমেন্টের প্রোফাইল এবং কন্টাক্ট ইনফরমেশন।"));

            builder.HasKey(x => x.Id);

            // ——— ApplicationUser ———
            builder.HasOne(x => x.ApplicationUser)
                   .WithMany()
                   .HasForeignKey(x => x.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.ApplicationUserId).IsUnique();

            builder.Property(x => x.LabName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.RegistrationNumber)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(x => x.RegistrationNumber).IsUnique();

            builder.Property(x => x.ContactNumber).HasMaxLength(20);
            builder.Property(x => x.LogoUrl).HasMaxLength(500);
            builder.Property(x => x.Address).HasMaxLength(500);
            builder.Property(x => x.City).HasMaxLength(100);
            builder.Property(x => x.AverageRating).HasColumnType("decimal(3,2)");
        }
    }
}