using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Application.DTOs.Pharmacy;

public class CreateMedicineRequestDto
{
    public Guid             PharmacyProfileId   { get; set; }
    public string           MedicineName        { get; set; } = string.Empty;
    public string           GenericName         { get; set; } = string.Empty;
    public string           BrandName           { get; set; } = string.Empty;
    public MedicineCategory Category            { get; set; }
    public string           Manufacturer        { get; set; } = string.Empty;
    public string           Strength            { get; set; } = string.Empty;
    public string           Unit                { get; set; } = string.Empty;
    public decimal          SellingPrice        { get; set; }
    public bool             RequiresPrescription { get; set; }
    public string?          Description         { get; set; }
    public string?          SideEffects         { get; set; }
    public string?          Contraindications   { get; set; }
    public string?          StorageConditions   { get; set; }
}
