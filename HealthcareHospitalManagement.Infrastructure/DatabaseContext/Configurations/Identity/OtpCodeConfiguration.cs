using HealthcareHospitalManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Identity
{
    public class OtpCodeConfiguration : IEntityTypeConfiguration<OtpCode>
    {
        public void Configure(EntityTypeBuilder<OtpCode> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("OtpCodes", "Identity", t => t.HasComment("ইউজার ভেরিফিকেশন এবং পাসওয়ার্ড রিসেটের জন্য ব্যবহৃত ওটিপি রেকর্ড।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(o => o.Id);


            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────

            builder.Property(o => o.CodeHash)
                   .IsRequired()
                   .HasMaxLength(256)
                   .HasComment("সিকিউরিটির জন্য ওটিপি কোডটি হ্যাশ করে সেভ করা হয়েছে।");

            builder.Property(o => o.SentTo)
                   .HasMaxLength(256)
                   .HasComment("ওটিপিটি কোন ইমেইল বা ফোন নম্বরে পাঠানো হয়েছে।");

            // এনাম কনভার্সন (Registration, PasswordReset ইত্যাদি)
            builder.Property(o => o.Purpose)
                   .HasConversion<string>()
                   .HasMaxLength(50);

            builder.Property(o => o.IpAddress).HasMaxLength(50);


            // ── ৪. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            builder.HasOne(o => o.User)
                   .WithMany(u => u.OtpCodes)
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Cascade); // ইউজার ডিলিট হলে ওটিপি হিস্ট্রি ডিলিট হবে


            // ── ৫. পারফরম্যান্স (Indexing) ─────────────────────────────────────────────────

            // ভেরিফিকেশনের সময় UserId এবং Purpose দিয়ে দ্রুত কোড খুঁজে পাওয়ার জন্য
            builder.HasIndex(o => new { o.UserId, o.Purpose, o.IsUsed });
        }
    }
}