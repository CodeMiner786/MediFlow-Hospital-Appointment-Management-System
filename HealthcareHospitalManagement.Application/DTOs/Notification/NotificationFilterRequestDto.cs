namespace HealthcareHospitalManagement.Application.DTOs.Notification;

public class NotificationFilterRequestDto
{
    public Guid     UserId      { get; set; }
    public bool?    IsRead      { get; set; }
    public int      PageNumber  { get; set; } = 1;
    public int      PageSize    { get; set; } = 20;
}
