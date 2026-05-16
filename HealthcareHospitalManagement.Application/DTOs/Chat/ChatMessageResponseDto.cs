using HealthcareHospitalManagement.Domain.Enums.Chat;

namespace HealthcareHospitalManagement.Application.DTOs.Chat;


public class ChatMessageResponseDto
{
    public Guid     Id                  { get; set; }
    public Guid     ConversationId      { get; set; }
    public Guid     SenderId            { get; set; }
    public string   SenderName          { get; set; } = string.Empty;
    public string   SenderType          { get; set; } = string.Empty;
    public string   Content             { get; set; } = string.Empty;
    public MessageStatus Status { get; set; }

    public string?  AttachmentUrl       { get; set; }
    public string?  AttachmentType      { get; set; }
    public DateTime SentAt              { get; set; }
    public DateTime? DeliveredAt        { get; set; }
    public DateTime? ReadAt             { get; set; }
}
