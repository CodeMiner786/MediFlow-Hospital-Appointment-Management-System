using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// আপনার ফোল্ডার স্ট্রাকচার অনুযায়ী নেমস্পেস আপডেট করা হলো
namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed.Posts
{
    public class PostCommentConfiguration : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
            builder.ToTable("PostComments", "Social", t => t.HasComment("পোস্টের বিপরীতে ইউজারদের করা কমেন্ট এবং রিপ্লাইগুলো এখানে সংরক্ষিত থাকে।"));


            // ── ২. কি এবং ইনডেক্স ──────────────────────────────────────────────────────────
            builder.HasKey(pc => pc.Id);


            // ── ৩. কলাম কনফিগারেশন ─────────────────────────────────────────────────────────

            // কমেন্টের মূল টেক্সট বা কন্টেন্ট
            builder.Property(pc => pc.Content)
                   .IsRequired()
                   .HasMaxLength(1500)
                   .HasComment("ইউজারের করা কমেন্টের টেক্সট কন্টেন্ট।");


            // ── ৪. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            // Post Interaction Relationship (Many-to-One)
            // একটি নির্দিষ্ট ইন্টারঅ্যাকশন হাবের অধীনে অনেকগুলো কমেন্ট থাকতে পারে
            builder.HasOne(pc => pc.PostInteraction)
                   .WithMany(pi => pi.Comments) // Interaction টেবিল থেকে আসা কালেকশন
                   .HasForeignKey(pc => pc.PostInteractionId) // এখানে 'Id' সহ প্রপার্টি হবে (আগে ভুল ছিল)
                   .OnDelete(DeleteBehavior.Cascade);


            // User Relationship (Many-to-One)
            builder.HasOne(pc => pc.User)
                   .WithMany()
                   .HasForeignKey(pc => pc.UserId)
                   .OnDelete(DeleteBehavior.Restrict);


            // Self-Referencing Relationship (Parent-Replies)
            builder.HasOne(pc => pc.ParentComment)
                   .WithMany(pc => pc.Replies)
                   .HasForeignKey(pc => pc.ParentCommentId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}