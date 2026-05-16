using HealthcareHospitalManagement.Domain.Entities.Feed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// যদি FeedItem ফাইলটি Feed ফোল্ডারের মেইন কনফিগারেশন হয়, তবে নেমস্পেস হবে এটি:
namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed
{
    public class FeedItemConfiguration : IEntityTypeConfiguration<FeedItem>
    {
        public void Configure(EntityTypeBuilder<FeedItem> builder)
        {
            // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
            builder.ToTable("FeedItems", "Social", t => t.HasComment("অ্যাপের মেইন ফিড বা টাইমলাইনে দেখানোর মতো আইটেমগুলোর তথ্য।"));


            // ── ২. কি এবং ইনডেক্স ──────────────────────────────────────────────────────────
            builder.HasKey(f => f.Id);


            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────

            builder.Property(f => f.Title)
                   .HasMaxLength(200)
                   .IsRequired()
                   .HasComment("ফিড আইটেমের মূল শিরোনাম।");

            builder.Property(f => f.SubTitle)
                   .HasMaxLength(500);

            builder.Property(f => f.Description)
                   .HasMaxLength(2000);

            builder.Property(f => f.City)
                   .HasMaxLength(100)
                   .HasComment("আইটেমটি যদি কোনো নির্দিষ্ট শহরের জন্য প্রাসঙ্গিক হয়।");

            // রেটিং কনফিগারেশন (যেমন: 4.50)
            builder.Property(f => f.AverageRating)
                   .HasPrecision(3, 2);


            // এনাম কনভার্সন (DB তে স্ট্রিং হিসেবে সেভ হবে)
            builder.Property(f => f.ItemType)
                   .HasConversion<string>()
                   .HasMaxLength(50);

            builder.Property(f => f.Status)
                   .HasConversion<string>()
                   .HasMaxLength(50);

            builder.Property(f => f.PrimaryAction)
                   .HasConversion<string>()
                   .HasMaxLength(50);


            // ── ৪. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            // আইটেমের মালিক (Owner)
            builder.HasOne(f => f.OwnerUser)
                   .WithMany()
                   .HasForeignKey(f => f.OwnerUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ট্যাগ কালেকশন (Shadow Property 'FeedItemId' ব্যবহার করে)
            builder.HasMany(f => f.Tags)
                   .WithOne()
                   .HasForeignKey("FeedItemId")
                   .OnDelete(DeleteBehavior.Cascade);

            // লাইক কালেকশন (Shadow Property 'FeedItemId' ব্যবহার করে)
            builder.HasMany(f => f.Likes)
                   .WithOne()
                   .HasForeignKey("FeedItemId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}