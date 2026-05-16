using HealthcareHospitalManagement.Domain.Entities.Lab;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.Configurations.Lab
{
    public class LabTestConfiguration : IEntityTypeConfiguration<LabTest>
    {
        public void Configure(EntityTypeBuilder<LabTest> builder)
        {
            builder.ToTable("LabTests", "Patient", t => t.HasComment("ল্যাবরেটরির অফার করা সুনির্দিষ্ট টেস্টগুলোর মাস্টার লিস্ট।"));

            builder.HasKey(x => x.Id);

            // ——— LabProfile → Tests ———
            builder.HasOne(x => x.LabProfile)
                   .WithMany(p => p.Tests)        // ← এখানে p.Tests specify করতে হবে
                   .HasForeignKey(x => x.LabProfileId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.TestName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.TestCode)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(x => x.TestCode).IsUnique();

            builder.Property(x => x.Category).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Unit).HasMaxLength(50);
            builder.Property(x => x.ReferenceRange).HasMaxLength(200);
            builder.Property(x => x.Preparation).HasMaxLength(500);
            builder.Property(x => x.EquipmentCode).HasMaxLength(100);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        }
    }
}