namespace HealthcareHospitalManagement.Application.DTOs.Identity;

public class UserLoginHistoryResponseDto
{
    public Guid     Id          { get; set; }
    public string   IpAddress   { get; set; } = string.Empty;
    public string   DeviceInfo  { get; set; } = string.Empty;
    public string   LoginProvider { get; set; } = string.Empty;
    public bool     IsSuccess   { get; set; }
    public string?  FailureReason { get; set; }
    public DateTime LoginAt     { get; set; }
}
