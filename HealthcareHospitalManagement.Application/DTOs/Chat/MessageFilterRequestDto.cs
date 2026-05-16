using HealthcareHospitalManagement.Domain.Enums.Chat;

namespace HealthcareHospitalManagement.Application.DTOs.Chat;

public class MessageFilterRequestDto
{
    public Guid     ConversationId  { get; set; }
    public int      PageNumber      { get; set; } = 1;
    public int      PageSize        { get; set; } = 20;
}
