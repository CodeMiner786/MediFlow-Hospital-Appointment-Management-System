using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed.Posts
{
    public class PostShareConfiguration : IEntityTypeConfiguration<PostShare>
    {
        public void Configure(EntityTypeBuilder<PostShare> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("PostShares", "Social", t => t.HasComment("ইউজারদের করা পোস্ট শেয়ারের রেকর্ড।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(ps => ps.Id);


            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────

            builder.Property(ps => ps.ShareNote)
                   .HasMaxLength(500)
                   .HasComment("শেয়ার করার সময় ইউজারের দেওয়া ঐচ্ছিক নোট।");


            // ── ৪. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            // Post Interaction Relationship (Many-to-One)
            builder.HasOne(ps => ps.PostInteraction)
                   .WithMany(pi => pi.Shares)
                   .HasForeignKey(ps => ps.PostInteractionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // User Relationship (Many-to-One)
            builder.HasOne(ps => ps.User)
                   .WithMany()
                   .HasForeignKey(ps => ps.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}