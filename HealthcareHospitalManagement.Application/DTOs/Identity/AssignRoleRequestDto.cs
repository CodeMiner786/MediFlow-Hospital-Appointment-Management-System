using HealthcareHospitalManagement.Domain.Enums.Permission;

namespace HealthcareHospitalManagement.Application.DTOs.Identity;

public class AssignRoleRequestDto
{
    public Guid     UserId      { get; set; }
    public PermissionAction? Action      { get; set; }
    public string   NewRole     { get; set; } = string.Empty;
    public string?   Reason      { get; set; } = string.Empty;
}
