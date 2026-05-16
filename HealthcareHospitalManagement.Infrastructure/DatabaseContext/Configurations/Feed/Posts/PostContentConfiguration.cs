using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// আপনার ফোল্ডার স্ট্রাকচার অনুযায়ী নেমস্পেস আপডেট করা হলো
namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed.Posts
{
    public class PostContentConfiguration : IEntityTypeConfiguration<PostContent>
    {
        public void Configure(EntityTypeBuilder<PostContent> builder)
        {
            // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
            builder.ToTable("PostContents", "Social", t => t.HasComment("পোস্টের মূল বিষয়বস্তু বা কন্টেন্ট (টেক্সট, ফরম্যাটেড টেক্সট এবং ল্যাঙ্গুয়েজ) এর তথ্য।"));


            // ── ২. কি এবং বেসিক কনফিগারেশন ──────────────────────────────────────────────────
            builder.HasKey(pc => pc.Id);


            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────

            // পোস্টের মূল টেক্সট (বড় পোস্টের জন্য ৫০০০ ক্যারেক্টার রাখা হয়েছে)
            builder.Property(pc => pc.TextBody)
                   .HasMaxLength(5000)
                   .HasComment("পোস্টের সাধারণ টেক্সট কন্টেন্ট।");

            // পোস্টটি কোন ভাষায় লেখা (English, Bengali ইত্যাদি)
            builder.Property(pc => pc.Language)
                   .HasMaxLength(20)
                   .HasComment("পোস্টের ভাষা।");

            // রিচ টেক্সট বা এইচটিএমএল কন্টেন্ট সংরক্ষণের জন্য
            builder.Property(pc => pc.FormattedContent)
                   .HasColumnType("nvarchar(max)")
                   .HasComment("ফরম্যাটেড কন্টেন্ট বা রিচ টেক্সট সংরক্ষণের জন্য।");

            // এনাম থেকে স্ট্রিং-এ রূপান্তর
            builder.Property(pc => pc.PostType)
                   .HasConversion<string>()
                   .HasMaxLength(50)
                   .HasComment("পোস্টের ধরন বা ক্যাটাগরি।");


            // ── ৪. রিলেশনশিপ কনফিগারেশন (One-to-One) ────────────────────────────────────────

            // Post to PostContent (One-to-One)
            builder.HasOne(pc => pc.Post)
                   .WithOne(p => p.Content)
                   .HasForeignKey<PostContent>(pc => pc.PostId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}