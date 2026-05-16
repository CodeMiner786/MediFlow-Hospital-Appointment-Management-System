using HealthcareHospitalManagement.Domain.Entities.Lab;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.Configurations.Lab
{
    public class LabOrderConfiguration : IEntityTypeConfiguration<LabOrder>
    {
        public void Configure(EntityTypeBuilder<LabOrder> builder)
        {
            builder.ToTable("LabOrders", "Patient", t =>
                    t.HasComment("ল্যাব টেস্টের অর্ডার এবং ক্লিনিকাল তথ্য।"));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderCode)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(x => x.OrderCode).IsUnique();

            // ——— Patient ———
            builder.HasOne(x => x.Patient)
                   .WithMany()
                   .HasForeignKey(x => x.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            // ——— Doctor (nullable) ———
            builder.HasOne(x => x.OrderedByDoctor)
                   .WithMany()
                   .HasForeignKey(x => x.OrderedByDoctorId)
                   .OnDelete(DeleteBehavior.SetNull);

            // ——— LabProfile ———
            builder.HasOne(x => x.LabProfile)
                   .WithMany()
                   .HasForeignKey(x => x.LabProfileId)
                   .OnDelete(DeleteBehavior.Restrict);

            // ——— Properties ———
            builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.ClinicalNotes).HasMaxLength(1000);
            builder.Property(x => x.Diagnosis).HasMaxLength(500);
            builder.Property(x => x.SpecialInstructions).HasMaxLength(500);
        }
    }
}