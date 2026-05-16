using HealthcareHospitalManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Identity;

public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.ToTable("UserRefreshTokens", "Identity",
            t => t.HasComment("ইউজারদের লগইন সেশন সচল রাখার জন্য রিফ্রেশ টোকেন স্টোর।"));

        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.Token)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(rt => rt.JwtId)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(rt => rt.ExpiresAt).IsRequired();
        builder.Property(rt => rt.IsRevoked).HasDefaultValue(false);
        builder.Property(rt => rt.IpAddress).HasMaxLength(100);
        builder.Property(rt => rt.UserAgent).HasMaxLength(200);

        // ── রিলেশনশিপ ──
        builder.HasOne(rt => rt.User)
               .WithMany(u => u.UserRefreshTokens)
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // ── ইনডেক্স (পারফরম্যান্সের জন্য জরুরি) ──
        builder.HasIndex(rt => rt.Token).IsUnique();
        builder.HasIndex(rt => rt.JwtId); // নতুন ইনডেক্স
    }
}