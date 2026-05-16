using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed.Posts
{
    public class PostInteractionConfiguration : IEntityTypeConfiguration<PostInteraction>
    {
        public void Configure(EntityTypeBuilder<PostInteraction> builder)
        {
            // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
            builder.ToTable("PostInteractions", "Social", t => t.HasComment("পোস্টের যাবতীয় ইন্টারেকশন (লাইক, কমেন্ট, শেয়ার, সেভ) এর সামারি এবং কালেকশন হাব।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(pi => pi.Id);


            // ── ৩. রিলেশনশিপ কনফিগারেশন (One-to-One with Post) ─────────────────────────────

            // একটি পোস্টের বিপরীতে শুধুমাত্র একটি ইন্টারেকশন সেন্টার থাকবে
            builder.HasOne(pi => pi.Post)
                   .WithOne(p => p.Interactions)
                   .HasForeignKey<PostInteraction>(pi => pi.PostId)
                   .OnDelete(DeleteBehavior.Cascade); // মূল পোস্ট মুছে গেলে তার সব ইন্টারেকশন রেকর্ডও মুছে যাবে


            // ── ৪. কালেকশন কনফিগারেশন (One-to-Many) ────────────────────────────────────────

            // Likes: পোস্টের লাইক কালেকশন
            // মনে রাখবেন: PostLike ক্লাসে 'PostInteractionId' প্রপার্টিটি থাকতে হবে
            builder.HasMany(pi => pi.Likes)
                   .WithOne(l => l.PostInteraction)
                   .HasForeignKey(l => l.PostInteractionId)
                   .OnDelete(DeleteBehavior.Cascade);


            // Comments: পোস্টের কমেন্ট কালেকশন
            // মনে রাখবেন: PostComment ক্লাসে 'PostInteractionId' প্রপার্টিটি থাকতে হবে
            builder.HasMany(pi => pi.Comments)
                   .WithOne(c => c.PostInteraction)
                   .HasForeignKey(c => c.PostInteractionId)
                   .OnDelete(DeleteBehavior.Cascade);


            // Shares: পোস্টের শেয়ার রেকর্ড
            builder.HasMany(pi => pi.Shares)
                   .WithOne(s => s.PostInteraction)
                   .HasForeignKey(s => s.PostInteractionId)
                   .OnDelete(DeleteBehavior.Cascade);


            // Saves: ইউজারদের সেভ করা রেকর্ড
            builder.HasMany(pi => pi.Saves)
                   .WithOne(s => s.PostInteraction)
                   .HasForeignKey(s => s.PostInteractionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}