using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed.Posts
{
    public class PostMediaConfiguration : IEntityTypeConfiguration<PostMedia>
    {
        public void Configure(EntityTypeBuilder<PostMedia> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("PostMedias", "Social", t => t.HasComment("পোস্টের সাথে যুক্ত ছবি বা ভিডিওর তথ্য।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(pm => pm.Id);


            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────

            builder.Property(pm => pm.MediaUrl)
                   .IsRequired()
                   .HasMaxLength(1000)
                   .HasComment("মিডিয়া ফাইলের (Image/Video) ডাইরেক্ট স্টোরেজ লিঙ্ক।");

            builder.Property(pm => pm.ThumbnailUrl)
                   .HasMaxLength(1000)
                   .HasComment("ভিডিওর ক্ষেত্রে থাম্বনেইল ছবির লিঙ্ক।");

            // এনাম কনভার্সন (DB তে স্ট্রিং হিসেবে সেভ হবে যেমন: 'Image', 'Video')
            builder.Property(pm => pm.MediaType)
                   .HasConversion<string>()
                   .HasMaxLength(50);

            builder.Property(pm => pm.SortOrder)
                   .HasDefaultValue(0)
                   .HasComment("একাধিক মিডিয়া থাকলে সেগুলোর প্রদর্শনের ক্রম।");


            // ── ৪. রিলেশনশিপ কনফিগারেশন (Many-to-One) ────────────────────────────────────────

            builder.HasOne(pm => pm.Post)
                   .WithMany(p => p.MediaFiles)
                   .HasForeignKey(pm => pm.PostId)
                   .OnDelete(DeleteBehavior.Cascade); // পোস্ট ডিলিট হলে মিডিয়া ডাটাও ডিলিট হবে
        }
    }
}