using HealthcareHospitalManagement.Domain.Enums.UserRole;

namespace HealthcareHospitalManagement.Application.DTOs.Auth;

public class ForgotPasswordRequestDto
{
    public string Email { get; set; } = string.Empty;
}
