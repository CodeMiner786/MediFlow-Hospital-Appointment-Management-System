using HealthcareHospitalManagement.Domain.Entities.Feed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// FeedItem এর সেভ কনফিগারেশন সাধারণত Feed ফোল্ডারের ভেতরে থাকে
namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Feed
{
    public class FeedItemSaveConfiguration : IEntityTypeConfiguration<FeedItemSave>
    {
        public void Configure(EntityTypeBuilder<FeedItemSave> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("FeedItemSaves", "Social", t => t.HasComment("ইউজারদের সেভ করা ফিড আইটেমগুলোর রেকর্ড।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(fs => fs.Id);


            // ── ৩. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            // FeedItem Relationship
            builder.HasOne(fs => fs.FeedItem)
                   .WithMany(f => f.Saves)
                   .HasForeignKey(fs => fs.FeedItemId)
                   .OnDelete(DeleteBehavior.Cascade); // আইটেম ডিলিট হলে সেভ করা রেকর্ডও মুছে যাবে


            
            // User Relationship - Cascade → NoAction
            builder.HasOne(fs => fs.User)
                   .WithMany()
                   .HasForeignKey(fs => fs.UserId)
                   .OnDelete(DeleteBehavior.NoAction); // ← Cascade ছিল, NoAction করো


            // ── ৪. ইনডেক্সিং ও কনস্ট্রেইন্ট ──────────────────────────────────────────────────

            // একই ইউজার যাতে একটি আইটেম বারবার সেভ করতে না পারেন
            builder.HasIndex(fs => new { fs.FeedItemId, fs.UserId })
                   .IsUnique()
                   .HasDatabaseName("IX_FeedItem_User_Unique_Save");
        }
    }
}