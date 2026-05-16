using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Patient;

public class UpdatePatientRequestDto
{
    public string?      AlternatePhone          { get; set; }
    public string?      Address                 { get; set; }
    public string?      City                    { get; set; }
    public decimal?     WeightKg                { get; set; }
    public decimal?     HeightCm                { get; set; }
    public string?      Allergies               { get; set; }
    public string?      ChronicDiseases         { get; set; }
    public string?      CurrentMedications      { get; set; }
    public string?      Notes                   { get; set; }
    public bool?        HasInsurance            { get; set; }
    public string?      InsuranceProvider       { get; set; }
    public string?      InsurancePolicyNumber   { get; set; }
    public DateTime?    InsuranceExpiryDate     { get; set; }
}
