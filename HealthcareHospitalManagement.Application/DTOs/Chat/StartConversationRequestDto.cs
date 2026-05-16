using HealthcareHospitalManagement.Domain.Enums.Chat;

namespace HealthcareHospitalManagement.Application.DTOs.Chat;

public class StartConversationRequestDto
{
    public Guid                 InitiatorUserId     { get; set; }
    public Guid                 RecipientUserId     { get; set; }
    public ConversationType     ConversationType    { get; set; }
    public string?              InitialMessage      { get; set; }
}
