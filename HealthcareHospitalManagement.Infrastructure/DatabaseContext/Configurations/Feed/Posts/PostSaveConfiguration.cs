using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed.Posts
{
    public class PostSaveConfiguration : IEntityTypeConfiguration<PostSave>
    {
        public void Configure(EntityTypeBuilder<PostSave> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("PostSaves", "Social", t => t.HasComment("ইউজারদের সেভ করে রাখা পোস্টগুলোর রেকর্ড।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(ps => ps.Id);


            // ── ৩. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            // Post Interaction Relationship (Many-to-One)
            builder.HasOne(ps => ps.PostInteraction)
                   .WithMany(pi => pi.Saves)
                   .HasForeignKey(ps => ps.PostInteractionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // User Relationship (Many-to-One)
            builder.HasOne(ps => ps.User)
                   .WithMany()
                   .HasForeignKey(ps => ps.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


            // ── ৪. ডাটা ইন্টিগ্রিটি ও ইনডেক্স ──────────────────────────────────────────────────

            // একই ইউজার যাতে একটি পোস্ট একবারই সেভ করতে পারেন (Unique Constraint)
            builder.HasIndex(ps => new { ps.PostInteractionId, ps.UserId })
                   .IsUnique()
                   .HasDatabaseName("IX_PostInteraction_User_Unique_Save");
        }
    }
}