using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Chat;

namespace HealthcareHospitalManagement.Domain.Entities.Chat;

public class Conversation : BaseEntity
{
    // ── ১. টাইপ এবং স্ট্যাটাস ────────────────────────────────────────────────────────
    public    ConversationType      Type                { get; set; }
    public    bool                  IsActive            { get; set; } = true;

    // ── ২. পার্টিসিপেন্ট A (সাধারণত পেশেন্ট/ইউজার) ────────────────────────────────────
    public    Guid                  ParticipantAId      { get; set; }
    public    ApplicationUser       ParticipantA        { get; set; } = null!;
    public    ChatParticipantType   ParticipantAType    { get; set; }

    // ── ৩. পার্টিসিপেন্ট B (ডাক্তার/অ্যাডমিন/প্রোভাইডার) ──────────────────────────────
    public    Guid                  ParticipantBId      { get; set; }
    public    ApplicationUser       ParticipantB        { get; set; } = null!;
    public    ChatParticipantType   ParticipantBType    { get; set; }

    // ── ৪. রেফারেন্স (Appointment/Booking) ──────────────────────────────────────────
    public    Guid?                 ReferenceId         { get; set; }
    public    string?               ReferenceType       { get; set; }

    // ── ৫. প্রিভিউ এবং লাস্ট মেসেজ ট্র্যাকিং ──────────────────────────────────────────
    public    DateTime?             LastMessageAt       { get; set; }
    public    string?               LastMessagePreview  { get; set; }

    // ── ৬. মেসেজ কালেকশন ─────────────────────────────────────────────────────────────
    public    ICollection<ChatMessage> Messages         { get; set; } = [];
}