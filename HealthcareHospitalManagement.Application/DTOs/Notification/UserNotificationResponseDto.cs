namespace HealthcareHospitalManagement.Application.DTOs.Notification;

public class UserNotificationResponseDto
{
    public Guid     Id                  { get; set; }
    public string   Title               { get; set; } = string.Empty;
    public string   Message             { get; set; } = string.Empty;
    public string   NotificationType    { get; set; } = string.Empty;
    public bool     IsRead              { get; set; }
    public string?  ActionUrl           { get; set; }
    public DateTime CreatedAt           { get; set; }
    public DateTime? ReadAt             { get; set; }
}
