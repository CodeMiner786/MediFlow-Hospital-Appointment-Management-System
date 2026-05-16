namespace HealthcareHospitalManagement.Application.DTOs.Identity;

public class RoleAssignmentLogResponseDto
{
    public Guid     Id              { get; set; }
    public string   UserName        { get; set; } = string.Empty;
    public string   OldRole         { get; set; } = string.Empty;
    public string   NewRole         { get; set; } = string.Empty;
    public string   AssignedByName  { get; set; } = string.Empty;
    public string   Reason          { get; set; } = string.Empty;
    public DateTime AssignedAt      { get; set; }
}
