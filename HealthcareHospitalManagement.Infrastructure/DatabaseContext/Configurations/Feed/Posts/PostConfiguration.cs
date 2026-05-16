using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed.Posts
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
            builder.ToTable("Posts", "Social", t => t.HasComment("ইউজারদের তৈরি করা বিভিন্ন ফিড পোস্ট (যেমন: হেলথ টিপস, জেনারেল পোস্ট) এর মূল তথ্য।"));


            // ── ২. কি এবং বেসিক কনফিগারেশন ──────────────────────────────────────────────────
            builder.HasKey(p => p.Id);


            // ── ৩. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            // One-to-One Relationship with PostInteraction
            // প্রতিটি পোস্টের জন্য একটি নির্দিষ্ট ইন্টারঅ্যাকশন হাব (Like, Comment Summary) থাকবে
            builder.HasOne(p => p.Interactions)
                   .WithOne(pi => pi.Post)
                   .HasForeignKey<PostInteraction>(pi => pi.PostId)
                   .OnDelete(DeleteBehavior.Cascade); // পোস্ট ডিলিট হলে সংশ্লিষ্ট সব ইন্টারঅ্যাকশন রেকর্ড অটো ডিলিট হবে


            // Post to Author (Many-to-One)
            // একজন ইউজার অনেকগুলো পোস্ট করতে পারেন, কিন্তু একটি পোস্টের একজনই অথর থাকবেন
            builder.HasOne(p => p.AuthorUser)
                   .WithMany()
                   .HasForeignKey(p => p.AuthorUserId)
                   .OnDelete(DeleteBehavior.Restrict); // অথর ডিলিট করার আগে তার পোস্টগুলো হ্যান্ডেল করতে হবে (নিরাপত্তার জন্য Restrict)


            // Post to LinkedFeedItem (Many-to-One / Optional)
            // পোস্টটি যদি অন্য কোনো ফিড আইটেমের সাথে যুক্ত থাকে
            builder.HasOne(p => p.LinkedFeedItem)
                   .WithMany()
                   .HasForeignKey(p => p.LinkedFeedItemId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull); // ফিড আইটেম ডিলিট হলেও পোস্টটি থেকে যাবে, শুধু লিংকটি নাল হবে


            // ── ৪. কালেকশন কনফিগারেশন ──────────────────────────────────────────────────────

            // Post to MediaFiles (One-to-Many)
            // একটি পোস্টে একাধিক ছবি বা ভিডিও থাকতে পারে
            builder.HasMany(p => p.MediaFiles)
                   .WithOne()
                   .HasForeignKey("PostId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}