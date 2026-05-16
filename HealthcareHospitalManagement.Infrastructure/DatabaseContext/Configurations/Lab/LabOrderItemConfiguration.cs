using HealthcareHospitalManagement.Domain.Entities.Lab;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.Configurations.Lab
{
    public class LabOrderItemConfiguration : IEntityTypeConfiguration<LabOrderItem>
    {
        public void Configure(EntityTypeBuilder<LabOrderItem> builder)
        {
            builder.ToTable("LabOrderItems", "Patient", t =>
                    t.HasComment("ল্যাব অর্ডারের অন্তর্গত প্রতিটি সুনির্দিষ্ট টেস্টের ফলাফল ও স্ট্যাটাস।"));

            builder.HasKey(x => x.Id);

            // ——— LabOrder → LabOrderItems (Cascade) ———
            builder.HasOne(x => x.LabOrder)
                   .WithMany(o => o.Items)
                   .HasForeignKey(x => x.LabOrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ——— LabTest → LabOrderItems (Restrict) ———
            builder.HasOne(x => x.LabTest)
                   .WithMany(t => t.OrderItems)
                   .HasForeignKey(x => x.LabTestId)
                   .OnDelete(DeleteBehavior.Restrict);

            // ——— Properties ———
            builder.Property(x => x.SampleCollectedBy).HasMaxLength(200);
            builder.Property(x => x.SampleBarcode).HasMaxLength(100);
            builder.Property(x => x.ResultValue).HasMaxLength(200);
            builder.Property(x => x.ResultUnit).HasMaxLength(50);
            builder.Property(x => x.ReferenceRange).HasMaxLength(200);
            builder.Property(x => x.Remarks).HasMaxLength(500);
            builder.Property(x => x.ResultEnteredBy).HasMaxLength(200);
            builder.Property(x => x.ReportUrl).HasMaxLength(500);
        }
    }
}