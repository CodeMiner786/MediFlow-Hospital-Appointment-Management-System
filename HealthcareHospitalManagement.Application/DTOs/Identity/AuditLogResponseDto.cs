namespace HealthcareHospitalManagement.Application.DTOs.Identity;

public class AuditLogResponseDto
{
    public Guid     Id          { get; set; }
    public string   UserName    { get; set; } = string.Empty;
    public string   Action      { get; set; } = string.Empty;
    public string   EntityName  { get; set; } = string.Empty;
    public Guid?    EntityId    { get; set; }
    public string?  OldValues   { get; set; }
    public string?  NewValues   { get; set; }
    public string?  IpAddress   { get; set; }
    public DateTime Timestamp   { get; set; }
}
