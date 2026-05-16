using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed.Posts
{
    public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("PostTags", "Social", t => t.HasComment("পোস্টের সাথে যুক্ত বিভিন্ন ট্যাগ বা কি-ওয়ার্ড।"));

            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(pt => pt.Id);

            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────
            builder.Property(pt => pt.Tag)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasComment("ট্যাগের নাম (যেমন: HealthTips, Cardiology)।");

            // ── ৪. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────
            // Post to PostTag (Many-to-One)
            builder.HasOne(pt => pt.Post)
                   .WithMany(p => p.Tags)
                   .HasForeignKey(pt => pt.PostId)
                   .OnDelete(DeleteBehavior.NoAction); // ← এখানে পরিবর্তন করা হলো

            // ── ৫. ইনডেক্সিং ও ডাটা ইন্টিগ্রিটি ──────────────────────────────────────────
            // একই পোস্টে যাতে ডুপ্লিকেট ট্যাগ না থাকে
            builder.HasIndex(pt => new { pt.PostId, pt.Tag })
                   .IsUnique()
                   .HasDatabaseName("IX_Post_Tag_Unique");
        }
    }
}
