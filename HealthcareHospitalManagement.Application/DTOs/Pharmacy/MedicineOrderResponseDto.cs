using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Application.DTOs.Pharmacy;

public class MedicineOrderResponseDto
{
    public Guid     Id              { get; set; }
    public string   OrderCode       { get; set; } = string.Empty;
    public string   PatientName     { get; set; } = string.Empty;
    public string   PharmacyName    { get; set; } = string.Empty;
    public string   Status          { get; set; } = string.Empty;
    public decimal  TotalAmount     { get; set; }
    public DateTime OrderDate       { get; set; }
    public string   DeliveryAddress { get; set; } = string.Empty;
}
