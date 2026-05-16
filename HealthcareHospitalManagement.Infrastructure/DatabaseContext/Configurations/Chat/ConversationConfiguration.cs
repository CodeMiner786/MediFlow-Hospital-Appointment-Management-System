using HealthcareHospitalManagement.Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Chat;

public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("Conversations", "Social", t => t.HasComment("ইউজারদের মধ্যকার চ্যাট থ্রেড বা কনভারসেশন লিস্ট।"));

        // ── ২. কি এবং ইনডেক্স (পারফরম্যান্স বুস্ট) ───────────────────────────────────────
        builder.HasKey(c => c.Id);

        // ইনডেক্স দেওয়া হচ্ছে যাতে ইউজাররা তাদের চ্যাট লিস্ট দ্রুত খুঁজে পায়
        builder.HasIndex(c => c.ParticipantAId);
        builder.HasIndex(c => c.ParticipantBId);

        // রেফারেন্স আইডি দিয়ে সার্চ করার জন্য ইনডেক্স
        builder.HasIndex(c => c.ReferenceId);

        // ── ৩. কলাম কনফিগারেশন ─────────────────────────────────────────────────────────
        builder.Property(c => c.ReferenceType).HasMaxLength(50);
        builder.Property(c => c.LastMessagePreview).HasMaxLength(500);

        // ── ৪. রিলেশনশিপ (Two Foreign Keys to Same Table) ──────────────────────────────

        // Participant A (User)
        builder.HasOne(c => c.ParticipantA)
               .WithMany()
               .HasForeignKey(c => c.ParticipantAId)
               .OnDelete(DeleteBehavior.Restrict);

        // Participant B (Doctor/Admin/etc.)
        builder.HasOne(c => c.ParticipantB)
               .WithMany()
               .HasForeignKey(c => c.ParticipantBId)
               .OnDelete(DeleteBehavior.Restrict);

        // Messages with Conversation (One-to-Many)
        builder.HasMany(c => c.Messages)
               .WithOne(m => m.Conversation)
               .HasForeignKey(m => m.ConversationId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}