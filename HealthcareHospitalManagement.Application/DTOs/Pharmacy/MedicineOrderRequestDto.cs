using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Application.DTOs.Pharmacy;

public class MedicineOrderRequestDto
{
    public Guid     PatientId       { get; set; }
    public Guid     PharmacyId      { get; set; }
    public Guid?    PrescriptionId  { get; set; }
    public string   DeliveryAddress { get; set; } = string.Empty;
    public List<MedicineOrderItemRequestDto> Items { get; set; } = [];
}
