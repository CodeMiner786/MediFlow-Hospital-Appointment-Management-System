using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// আপনার ফোল্ডার স্ট্রাকচার অনুযায়ী নেমস্পেস আপডেট করা হলো
namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed.Posts
{
    public class PostAudienceConfiguration : IEntityTypeConfiguration<PostAudience>
    {
        public void Configure(EntityTypeBuilder<PostAudience> builder)
        {
            // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
            builder.ToTable("PostAudiences", "Social", t => t.HasComment("একটি নির্দিষ্ট পোস্টের অডিয়েন্স সেটিংস এবং ভিজিবিলিটি (কে কে দেখতে পারবে) সংক্রান্ত তথ্য।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(pa => pa.Id);


            // ── ৩. কলাম কনফিগারেশন ─────────────────────────────────────────────────────────

            // পোস্টের ভিজিবিলিটি স্ট্যাটাস (Public, Private, etc.) এনাম থেকে স্ট্রিং-এ রূপান্তর
            builder.Property(pa => pa.Visibility)
                   .HasConversion<string>()
                   .HasMaxLength(50)
                   .HasComment("পোস্টের ভিজিবিলিটি লেভেল (যেমন: Public, Private, Anonymous)।");

            // টার্গেট সিটি (যদি পোস্টটি নির্দিষ্ট কোনো শহরের জন্য হয়)
            builder.Property(pa => pa.TargetCity)
                   .HasMaxLength(100)
                   .HasComment("যদি নির্দিষ্ট কোনো শহরের অডিয়েন্সকে টার্গেট করা হয়।");

            // টার্গেট স্পেশালিটি (যেমন: শুধুমাত্র ডক্টরদের জন্য)
            builder.Property(pa => pa.TargetSpecialty)
                   .HasMaxLength(100)
                   .HasComment("যদি নির্দিষ্ট কোনো মেডিকেল স্পেশালিটির ইউজারদের জন্য পোস্টটি করা হয়।");


            // ── ৪. রিলেশনশিপ (One-to-One) ──────────────────────────────────────────────────

            // Post to PostAudience (One-to-One)
            // একটি পোস্টের বিপরীতে শুধুমাত্র একটি অডিয়েন্স কনফিগারেশন থাকবে
            builder.HasOne(pa => pa.Post)
                   .WithOne(p => p.Audience)
                   .HasForeignKey<PostAudience>(pa => pa.PostId)
                   .OnDelete(DeleteBehavior.Cascade); // মেইন পোস্ট ডিলিট হলে অডিয়েন্স সেটিংসও অটো ডিলিট হয়ে যাবে
        }
    }
}