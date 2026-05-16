using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed.Posts
{
    public class PostLikeConfiguration : IEntityTypeConfiguration<PostLike>
    {
        public void Configure(EntityTypeBuilder<PostLike> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("PostLikes", "Social", t => t.HasComment("পোস্টের বিপরীতে ইউজারদের দেওয়া লাইক বা রিঅ্যাকশন।"));

            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(pl => pl.Id);

            // ── ৩. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            // Post Interaction Relationship (Many-to-One)
            // অনেকগুলো লাইক একটি নির্দিষ্ট পোস্ট ইন্টারঅ্যাকশন হাবের অধীনে থাকে
            builder.HasOne(pl => pl.PostInteraction)
                   .WithMany(pi => pi.Likes)
                   .HasForeignKey(pl => pl.PostInteractionId)
                   .OnDelete(DeleteBehavior.Cascade); // পোস্ট ডিলিট হলে সব লাইক রেকর্ড মুছে যাবে

            // User Relationship (Many-to-One)
            // একজন ইউজার অনেকগুলো পোস্টে লাইক দিতে পারেন
            builder.HasOne(pl => pl.User)
                   .WithMany()
                   .HasForeignKey(pl => pl.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ── ৪. ডাটা ইন্টিগ্রিটি ও পারফরম্যান্স ──────────────────────────────────────────

            // একই ইউজার যাতে একটি পোস্টে একবারের বেশি লাইক দিতে না পারেন (Unique Constraint)
            builder.HasIndex(pl => new { pl.PostInteractionId, pl.UserId })
                   .IsUnique()
                   .HasDatabaseName("IX_PostInteraction_User_Unique_Like");
        }
    }
}