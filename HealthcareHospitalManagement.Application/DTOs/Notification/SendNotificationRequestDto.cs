namespace HealthcareHospitalManagement.Application.DTOs.Notification;

public class SendNotificationRequestDto
{
    public Guid     UserId          { get; set; }
    public string   Title           { get; set; } = string.Empty;
    public string   Message         { get; set; } = string.Empty;
    public string   NotificationType { get; set; } = string.Empty;
    public string?  ActionUrl        { get; set; }
}
