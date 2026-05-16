namespace HealthcareHospitalManagement.Application.DTOs.Identity;

public class VerifyOtpRequestDto
{
    public Guid     UserId      { get; set; }
    public string   OtpCode     { get; set; } = string.Empty;
    public string   Purpose     { get; set; } = string.Empty; // EmailVerification, PhoneVerification, PasswordReset
}
