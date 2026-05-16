using HealthcareHospitalManagement.Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Chat;

public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("ChatMessages", "Social", t => t.HasComment("সিস্টেমের সব ইউজারদের মধ্যকার চ্যাট মেসেজ স্টোর করার টেবিল।"));

        // ── ২. কি এবং ইনডেক্স (পারফরম্যান্সের জন্য জরুরি) ──────────────────────────────
        builder.HasKey(m => m.Id);

        // চ্যাট লোড করার সময় ConversationId দিয়ে ফিল্টার হয়, তাই এখানে ইনডেক্স প্রয়োজন
        builder.HasIndex(m => m.ConversationId);
        builder.HasIndex(m => m.SenderId);

        // ── ৩. কলাম কনফিগারেশন ─────────────────────────────────────────────────────────

        // মেসেজ কন্টেন্ট অনেক বড় হতে পারে তাই nvarchar(max) বা বড় লেন্থ দেওয়া ভালো
        builder.Property(m => m.Content)
               .IsRequired()
               .HasMaxLength(2000);

        builder.Property(m => m.AttachmentUrl).HasMaxLength(500);
        builder.Property(m => m.AttachmentType).HasMaxLength(50);

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────

        // Conversation to Messages (One-to-Many)
        builder.HasOne(m => m.Conversation)
               .WithMany(c => c.Messages)
               .HasForeignKey(m => m.ConversationId)
               .OnDelete(DeleteBehavior.Cascade); // কনভারসেশন ডিলিট করলে মেসেজও ডিলিট হবে

        // Sender (ApplicationUser) to Messages
        builder.HasOne(m => m.Sender)
               .WithMany()
               .HasForeignKey(m => m.SenderId)
               .OnDelete(DeleteBehavior.Restrict); // ইউজার ডিলিট করলে মেসেজ ডিলিট হবে না
    }
}