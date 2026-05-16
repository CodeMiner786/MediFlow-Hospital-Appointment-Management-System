using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Chat;

namespace HealthcareHospitalManagement.Domain.Entities.Chat;

public class ChatMessage : BaseEntity
{
    // ── ১. রিলেশন (Conversation) ──────────────────────────────────────────────────
    public    Guid                  ConversationId      { get; set; }
    public    Conversation          Conversation        { get; set; } = null!;

    // ── ২. সেন্ডার ডিটেইলস ────────────────────────────────────────────────────────
    public    Guid                  SenderId            { get; set; }
    public    ApplicationUser       Sender              { get; set; } = null!;
    public    ChatParticipantType   SenderType          { get; set; }

    // ── ৩. মেসেজ কন্টেন্ট ──────────────────────────────────────────────────────────
    public    string                Content             { get; set; } = string.Empty;
    public    MessageStatus         Status              { get; set; } = MessageStatus.Sent;

    // ── ৪. অ্যাটাচমেন্ট (Optional) ──────────────────────────────────────────────────
    public    string?               AttachmentUrl       { get; set; }
    public    string?               AttachmentType      { get; set; } // image, pdf, audio
    public    long?                 AttachmentSizeBytes { get; set; }

    // ── ৫. ট্র্যাকিং ডেট ───────────────────────────────────────────────────────────
    public    DateTime?             DeliveredAt         { get; set; }
    public    DateTime?             ReadAt              { get; set; }
}