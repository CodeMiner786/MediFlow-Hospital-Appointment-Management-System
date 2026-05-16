using HealthcareHospitalManagement.Domain.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Notification
{
    public class NotificationTemplateConfiguration : IEntityTypeConfiguration<NotificationTemplate>
    {
        public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
        {
            builder.ToTable("NotificationTemplates", "Social", t => t.HasComment("ইমেইল, এসএমএস এবং পুশ নোটিফিকেশনের ডাইনামিক টেমপ্লেট স্টোর।"));

            builder.HasKey(nt => nt.Id);

            // টেমপ্লেট কোড ইউনিক হওয়া জরুরি যাতে দ্রুত খুঁজে পাওয়া যায়
            builder.Property(nt => nt.TemplateCode).IsRequired().HasMaxLength(100);
            builder.HasIndex(nt => nt.TemplateCode).IsUnique();

            builder.Property(nt => nt.Name).IsRequired().HasMaxLength(200);
            builder.Property(nt => nt.SubjectEn).HasMaxLength(300);
            builder.Property(nt => nt.SubjectBn).HasMaxLength(400);
            builder.Property(nt => nt.BodyEn).IsRequired(); // বড় টেক্সট হতে পারে
            builder.Property(nt => nt.BodyBn).IsRequired();

            // এনাম কনভার্সন
            builder.Property(nt => nt.Channel).HasConversion<string>().HasMaxLength(50);
            builder.Property(nt => nt.Category).HasConversion<string>().HasMaxLength(50);
        }
    }
}