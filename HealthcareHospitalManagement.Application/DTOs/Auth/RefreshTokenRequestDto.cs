using HealthcareHospitalManagement.Domain.Enums.UserRole;

namespace HealthcareHospitalManagement.Application.DTOs.Auth;

public class RefreshTokenRequestDto
{
    public string AccessToken  { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
