using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using HealthcareHospitalManagement.Domain.Enums.Feed.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed.Posts;

public class PostMetaConfiguration : IEntityTypeConfiguration<PostMeta>
{
    public void Configure(EntityTypeBuilder<PostMeta> builder)
    {
        // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
        builder.ToTable("PostMetas", "Social", t =>
            t.HasComment("পোস্টের মেটা ডাটা যেমন স্ট্যাটাস, পিনিং এবং পাবলিশ টাইম।"));

        // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
        builder.HasKey(pm => pm.Id);

        // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────
        builder.Property(pm => pm.Status)
               .HasConversion<string>()
               .HasMaxLength(30)
               .HasDefaultValue(PostStatus.Draft)
               .HasSentinel(PostStatus.Draft);

        builder.Property(pm => pm.IsPinned)
               .HasDefaultValue(false)
               .HasComment("পোস্টটি ফিডের ওপরে পিন করা থাকবে কি না।");

        builder.Property(pm => pm.PublishedAt)
               .IsRequired(false)
               .HasComment("পোস্টটি ঠিক কখন পাবলিশ করা হয়েছে।");

        // ── ৪. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────
        builder.HasOne(pm => pm.Post)
               .WithOne(p => p.Meta)
               .HasForeignKey<PostMeta>(pm => pm.PostId)
               .OnDelete(DeleteBehavior.Cascade);

        // Tags (One-to-Many) → এখানে Cascade বাদ দেওয়া হলো
        builder.HasMany(pm => pm.Tags)
               .WithOne()
               .HasForeignKey("PostMetaId");

        // ── ৫. ইনডেক্সিং ───────────────────────────────────────────────────────────────
        builder.HasIndex(pm => pm.PostId).IsUnique();
        builder.HasIndex(pm => pm.Status);
    }
}
