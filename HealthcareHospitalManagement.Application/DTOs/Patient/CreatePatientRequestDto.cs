using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Patient;

public class CreatePatientRequestDto
{
    public Guid         ApplicationUserId       { get; set; }
    public string       FirstName               { get; set; } = string.Empty;
    public string       LastName                { get; set; } = string.Empty;
    public DateTime     DateOfBirth             { get; set; }
    public Gender       Gender                  { get; set; }
    public BloodGroup   BloodGroup              { get; set; } = BloodGroup.Unknown;
    public MaritalStatus MaritalStatus          { get; set; }
    public string       NationalId              { get; set; } = string.Empty;
    public string       PhoneNumber             { get; set; } = string.Empty;
    public string?      AlternatePhone          { get; set; }
    public string       Email                   { get; set; } = string.Empty;
    public string       Address                 { get; set; } = string.Empty;
    public string       City                    { get; set; } = string.Empty;
    public string       State                   { get; set; } = string.Empty;
    public string       ZipCode                 { get; set; } = string.Empty;
    public string       Country                 { get; set; } = string.Empty;
    public string       EmergencyContactName    { get; set; } = string.Empty;
    public string       EmergencyContactPhone   { get; set; } = string.Empty;
    public string       EmergencyContactRelation { get; set; } = string.Empty;
    public string?      Allergies               { get; set; }
    public string?      ChronicDiseases         { get; set; }
    public string?      CurrentMedications      { get; set; }
    public bool         HasInsurance            { get; set; }
    public string?      InsuranceProvider       { get; set; }
    public string?      InsurancePolicyNumber   { get; set; }
    public DateTime?    InsuranceExpiryDate     { get; set; }
}
