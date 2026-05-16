using HealthcareHospitalManagement.Domain.Enums.UserRole;

namespace HealthcareHospitalManagement.Application.DTOs.Auth;

public class AuthResponseDto
{
    public string   AccessToken     { get; set; } = string.Empty;
    public string   RefreshToken    { get; set; } = string.Empty;
    public DateTime ExpiresAt       { get; set; }
    public string   Role            { get; set; } = string.Empty;
    public Guid     UserId          { get; set; }
    public string   FullName        { get; set; } = string.Empty;
    public string?  ProfileImageUrl { get; set; }
}
