using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Patient;

public class PatientDetailResponseDto : PatientSummaryResponseDto
{
    public string   Email               { get; set; } = string.Empty;
    public string   Address             { get; set; } = string.Empty;
    public string   NationalId          { get; set; } = string.Empty;
    public string   MaritalStatus       { get; set; } = string.Empty;
    public string   PatientType         { get; set; } = string.Empty;
    public decimal? WeightKg            { get; set; }
    public decimal? HeightCm            { get; set; }
    public decimal? BMI                 { get; set; }
    public string?  Allergies           { get; set; }
    public string?  ChronicDiseases     { get; set; }
    public string?  CurrentMedications  { get; set; }
    public bool     HasInsurance        { get; set; }
    public string?  InsuranceProvider   { get; set; }
    public string   EmergencyContactName  { get; set; } = string.Empty;
    public string   EmergencyContactPhone { get; set; } = string.Empty;
}
