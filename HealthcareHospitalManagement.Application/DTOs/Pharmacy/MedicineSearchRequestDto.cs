using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Application.DTOs.Pharmacy;

public class MedicineSearchRequestDto
{
    public string?          Name        { get; set; }
    public MedicineCategory? Category   { get; set; }
    public string?          GenericName { get; set; }
    public bool?            RequiresPrescription { get; set; }
    public int              PageNumber  { get; set; } = 1;
    public int              PageSize    { get; set; } = 10;
}
