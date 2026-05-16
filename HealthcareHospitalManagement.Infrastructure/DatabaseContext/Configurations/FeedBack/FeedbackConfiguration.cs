using HealthcareHospitalManagement.Domain.Entities.Feedback;
using HealthcareHospitalManagement.Domain.Enums.FeedbackRating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feedback
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<FeedbackEntity>
    {
        public void Configure(EntityTypeBuilder<FeedbackEntity> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("Feedbacks", "Social", t => t.HasComment("পেশেন্টদের পক্ষ থেকে বিভিন্ন সেবার ফিডব্যাক ও রেটিং।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(f => f.Id);


            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────

            builder.Property(f => f.Comment)
                   .HasMaxLength(2000)
                   .HasComment("পেশেন্টের দেওয়া বিস্তারিত মন্তব্য।");

            builder.Property(f => f.Rating)
                   .IsRequired()
                   .HasComment("সেবার মান রেটিং (১ থেকে ৫)।");

            // এনাম কনভার্সন
            builder.Property(f => f.FeedbackType)
                   .HasConversion<string>()
                   .HasMaxLength(50);

            builder.Property(f => f.Status)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .HasDefaultValue(FeedbackStatus.Pending);

            builder.Property(f => f.AdminResponse)
                   .HasMaxLength(2000)
                   .HasComment("কর্তৃপক্ষের পক্ষ থেকে দেওয়া উত্তর।");


            // ── ৪. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            // Patient Relationship
            builder.HasOne(f => f.Patient)
                   .WithMany() // যদি Patient এনটিটিতে ICollection<FeedbackEntity> থাকে তবে এখানে যোগ করুন
                   .HasForeignKey(f => f.PatientId)
                   .OnDelete(DeleteBehavior.Cascade);

            // নোট: Doctor, Lab, Pharmacy ইত্যাদির সাথে আপনার ডোমেইন মডেল অনুযায়ী রিলেশন করা যাবে
            // যেহেতু এগুলো Guid?, তাই এগুলো নাল হিসেবে থাকতে পারবে।
        }
    }
}