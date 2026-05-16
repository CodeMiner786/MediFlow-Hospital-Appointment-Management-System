using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Application.DTOs.Pharmacy;

public class MedicineResponseDto
{
    public Guid     Id                  { get; set; }
    public string   MedicineName        { get; set; } = string.Empty;
    public string   GenericName         { get; set; } = string.Empty;
    public string   BrandName           { get; set; } = string.Empty;
    public string   MedicineCode        { get; set; } = string.Empty;
    public string   Category            { get; set; } = string.Empty;
    public string   Strength            { get; set; } = string.Empty;
    public string   Unit                { get; set; } = string.Empty;
    public decimal  SellingPrice        { get; set; }
    public bool     RequiresPrescription { get; set; }
    public int      StockQuantity       { get; set; }
    public string?  ImageUrl            { get; set; }
}
