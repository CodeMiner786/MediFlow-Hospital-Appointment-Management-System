using HealthcareHospitalManagement.Domain.Entities.Feed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// FeedItem এবং এর রিলেটেড ফাইলগুলো সাধারণত Feed ফোল্ডারের মূলে থাকে
namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed
{
    public class FeedItemLikeConfiguration : IEntityTypeConfiguration<FeedItemLike>
    {
        public void Configure(EntityTypeBuilder<FeedItemLike> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("FeedItemLikes", "Social", t => t.HasComment("ফিড আইটেমগুলোতে ইউজারদের দেওয়া লাইক বা রিঅ্যাকশন।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(fl => fl.Id);


            // ── ৩. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            // FeedItem Relationship
            // একটি লাইক অবশ্যই একটি ফিড আইটেমের সাথে যুক্ত থাকতে হবে
            builder.HasOne(fl => fl.FeedItem)
                   .WithMany(f => f.Likes)
                   .HasForeignKey(fl => fl.FeedItemId)
                   .OnDelete(DeleteBehavior.Cascade); // আইটেম ডিলিট হলে লাইকও ডিলিট হবে


            // User Relationship
            // কোন ইউজার লাইক দিয়েছেন
            // User Relationship - Cascade → NoAction এ বদলাও
            builder.HasOne(fl => fl.User)
                   .WithMany()
                   .HasForeignKey(fl => fl.UserId)
                   .OnDelete(DeleteBehavior.NoAction); // ← Cascade ছিল, NoAction করো


            // ── ৪. পারফরম্যান্স এবং ডাটা ইন্টিগ্রিটি ──────────────────────────────────────────

            // Unique Index: একই ইউজার যাতে একটি আইটেমে একাধিকবার লাইক না দিতে পারেন
            builder.HasIndex(fl => new { fl.FeedItemId, fl.UserId })
                   .IsUnique()
                   .HasDatabaseName("IX_FeedItem_User_Unique_Like");
        }
    }
}