using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Application.DTOs.Pharmacy;

public class PrescriptionResponseDto
{
    public Guid     Id              { get; set; }
    public string   PatientName     { get; set; } = string.Empty;
    public string   DoctorName      { get; set; } = string.Empty;
    public string   Diagnosis       { get; set; } = string.Empty;
    public string?  Notes           { get; set; }
    public DateTime IssuedAt        { get; set; }
    public List<PrescriptionItemResponseDto> Items { get; set; } = [];
}
