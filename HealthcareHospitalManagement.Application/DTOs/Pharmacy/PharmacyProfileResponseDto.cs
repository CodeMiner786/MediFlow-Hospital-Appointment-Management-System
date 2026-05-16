using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Application.DTOs.Pharmacy;

public class PharmacyProfileResponseDto
{
    public Guid     Id              { get; set; }
    public string   PharmacyName    { get; set; } = string.Empty;
    public string   LicenseNumber   { get; set; } = string.Empty;
    public string   ContactNumber   { get; set; } = string.Empty;
    public string   Address         { get; set; } = string.Empty;
    public string   City            { get; set; } = string.Empty;
    public bool     IsVerified      { get; set; }
    public decimal  AverageRating   { get; set; }
    public bool     IsOpen24Hours   { get; set; }
}
