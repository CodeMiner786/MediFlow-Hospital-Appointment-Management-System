using HealthcareHospitalManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Identity
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users", "Identity", t => t.HasComment("সিস্টেমের সকল ইউজার এবং তাদের সিকিউরিটি ক্রেডেনশিয়াল।"));

            builder.HasKey(u => u.Id);

            // ── প্রোফাইল ও ইনডেক্সিং ───────────────────────────────────
            builder.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(100).IsRequired();

            // ইমেইল ইউনিক ইনডেক্স (খুবই জরুরি)
            builder.Property(u => u.Email).HasMaxLength(256).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

            builder.Property(u => u.PhoneNumber).HasMaxLength(20);

            // ── এনাম কনভার্সন ───────────────────────────────────────
            builder.Property(u => u.Role)
                   .HasConversion<string>()
                   .HasMaxLength(50);

            builder.Property(u => u.AccountStatus)
                   .HasConversion<string>()
                   .HasMaxLength(30);

            builder.Property(u => u.LoginProvider)
                   .HasConversion<string>()
                   .HasMaxLength(50);

            // ── রিলেশনশিপস ─────────────────────────────────────────

            // One-to-Many: RefreshTokens
            builder.HasMany(u => u.UserRefreshTokens)
                   .WithOne(t => t.User)
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: LoginHistories
            builder.HasMany(u => u.LoginHistories)
                   .WithOne()
                   .HasForeignKey("UserId")
                   .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: Notifications
            builder.HasMany(u => u.Notifications)
                   .WithOne()
                   .HasForeignKey("UserId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}