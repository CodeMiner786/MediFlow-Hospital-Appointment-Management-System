namespace HealthcareHospitalManagement.Application.DTOs.Notification;

public class NotificationTemplateResponseDto
{
    public Guid     Id          { get; set; }
    public string   TemplateName { get; set; } = string.Empty;
    public string   Subject     { get; set; } = string.Empty;
    public string   Body        { get; set; } = string.Empty;
    public string   Channel     { get; set; } = string.Empty;
}
