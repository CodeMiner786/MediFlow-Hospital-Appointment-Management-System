using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Patient;

public class PatientSummaryResponseDto
{
    public Guid     Id          { get; set; }
    public string   PatientCode { get; set; } = string.Empty;
    public string   FullName    { get; set; } = string.Empty;
    public int      Age         { get; set; }
    public string   Gender      { get; set; } = string.Empty;
    public string   BloodGroup  { get; set; } = string.Empty;
    public string   PhoneNumber { get; set; } = string.Empty;
    public string?  ProfileImageUrl { get; set; }
}
