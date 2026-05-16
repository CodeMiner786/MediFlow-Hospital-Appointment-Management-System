using HealthcareHospitalManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Identity
{
    public class PasswordResetRequestConfiguration : IEntityTypeConfiguration<PasswordResetRequest>
    {
        public void Configure(EntityTypeBuilder<PasswordResetRequest> builder)
        {
            // ── ১. টেবিল কনফিগারেশন ────────────────────────────────────────────────────────
            builder.ToTable("PasswordResetRequests", "Identity", t => t.HasComment("ইউজারদের পাসওয়ার্ড রিসেট করার টোকেন ও রিকোয়েস্ট ট্র্যাকিং।"));


            // ── ২. প্রাইমারি কি ────────────────────────────────────────────────────────────
            builder.HasKey(pr => pr.Id);


            // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────────────────────

            builder.Property(pr => pr.TokenHash)
                   .IsRequired()
                   .HasMaxLength(256)
                   .HasComment("নিরাপত্তার জন্য রিসেট টোকেনটি হ্যাশ করে রাখা হয়েছে।");

            builder.Property(pr => pr.IpAddress).HasMaxLength(50);

            builder.Property(pr => pr.UserAgent).HasMaxLength(500);


            // ── ৪. রিলেশনশিপ কনফিগারেশন ────────────────────────────────────────────────────

            builder.HasOne(pr => pr.User)
                   .WithMany() // সাধারণত অনেকগুলো রিকোয়েস্ট থাকতে পারে এক ইউজারের
                   .HasForeignKey(pr => pr.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


            // ── ৫. পারফরম্যান্স (Indexing) ─────────────────────────────────────────────────

            // টোকেন দিয়ে সার্চ করার সময় পারফরম্যান্স বাড়ানোর জন্য
            builder.HasIndex(pr => pr.TokenHash).IsUnique();

            // ইউজারের লেটেস্ট রিকোয়েস্ট দ্রুত খুঁজে পাওয়ার জন্য
            builder.HasIndex(pr => new { pr.UserId, pr.IsUsed });
        }
    }
}