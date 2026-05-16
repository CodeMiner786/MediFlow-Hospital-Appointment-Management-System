using HealthcareHospitalManagement.Domain.Entities.Feed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// FeedItem এর ট্যাগ কনফিগারেশন সাধারণত Feed ফোল্ডারের ভেতরে থাকে (Posts-এর বাইরে)
namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed
{
    public class FeedItemTagConfiguration : IEntityTypeConfiguration<FeedItemTag>
    {
        public void Configure(EntityTypeBuilder<FeedItemTag> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("FeedItemTags", "Social", t => t.HasComment("ফিড আইটেমগুলোতে ব্যবহৃত বিভিন্ন ট্যাগ (যেমন: Health, Medical) এর রেকর্ড।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(ft => ft.Id);


            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────

            builder.Property(ft => ft.Tag)
                   .HasMaxLength(50)
                   .IsRequired()
                   .HasComment("ট্যাগের নাম বা টেক্সট।");


            // ── ৪. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            // FeedItem Relationship
            builder.HasOne(ft => ft.FeedItem)
                   .WithMany(f => f.Tags)
                   .HasForeignKey(ft => ft.FeedItemId)
                   .OnDelete(DeleteBehavior.Cascade); // আইটেম ডিলিট হলে ট্যাগগুলোও ডিলিট হবে


            // ── ৫. পারফরম্যান্স এবং ডাটা ইন্টিগ্রিটি ──────────────────────────────────────────

            // Unique Index: একই পোস্টে যাতে একই ট্যাগ দুইবার না আসে
            builder.HasIndex(ft => new { ft.FeedItemId, ft.Tag })
                   .IsUnique()
                   .HasDatabaseName("IX_FeedItem_Tag_Unique");
        }
    }
}