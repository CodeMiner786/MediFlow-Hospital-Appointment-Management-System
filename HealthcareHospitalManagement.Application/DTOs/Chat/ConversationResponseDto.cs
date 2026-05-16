using HealthcareHospitalManagement.Domain.Enums.Chat;

namespace HealthcareHospitalManagement.Application.DTOs.Chat;

public class ConversationResponseDto
{
    public Guid     Id                  { get; set; }
    public ConversationType ConversationType { get; set; }

    public string   ParticipantName     { get; set; } = string.Empty;
    public string?  ParticipantImageUrl { get; set; }
    public string?  LastMessage         { get; set; }
    public DateTime? LastMessageAt      { get; set; }
    public int      UnreadCount         { get; set; }
    public bool     IsActive            { get; set; }
}
