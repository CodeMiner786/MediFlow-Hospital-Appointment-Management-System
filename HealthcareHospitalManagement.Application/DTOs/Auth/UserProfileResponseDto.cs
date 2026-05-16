using HealthcareHospitalManagement.Domain.Enums.UserRole;

namespace HealthcareHospitalManagement.Application.DTOs.Auth;

public class UserProfileResponseDto
{
    public Guid     Id              { get; set; }
    public string   FirstName       { get; set; } = string.Empty;
    public string   LastName        { get; set; } = string.Empty;
    public string   FullName        { get; set; } = string.Empty;
    public string   Email           { get; set; } = string.Empty;
    public string?  PhoneNumber     { get; set; }
    public string   Role            { get; set; } = string.Empty;
    public string   AccountStatus   { get; set; } = string.Empty;
    public bool     IsEmailVerified { get; set; }
    public string?  ProfileImageUrl { get; set; }
    public DateTime? LastLoginAt    { get; set; }
}
