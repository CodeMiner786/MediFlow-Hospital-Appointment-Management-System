using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Application.DTOs.Pharmacy;

public class PrescriptionItemResponseDto
{
    public string   MedicineName    { get; set; } = string.Empty;
    public string   GenericName     { get; set; } = string.Empty;
    public string   Dosage          { get; set; } = string.Empty;
    public string   Frequency       { get; set; } = string.Empty;
    public int      DurationDays    { get; set; }
    public string?  Instructions    { get; set; }
}
