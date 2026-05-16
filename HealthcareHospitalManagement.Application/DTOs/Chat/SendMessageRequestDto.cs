using HealthcareHospitalManagement.Domain.Enums.Chat;

namespace HealthcareHospitalManagement.Application.DTOs.Chat;

public class SendMessageRequestDto
{
    public Guid                 ConversationId      { get; set; }
    public Guid                 SenderId            { get; set; }
    public ChatParticipantType  SenderType          { get; set; }
    public string               Content             { get; set; } = string.Empty;
    public string?              AttachmentUrl       { get; set; }
    public string?              AttachmentType      { get; set; }
    public long?                AttachmentSizeBytes { get; set; }
}
