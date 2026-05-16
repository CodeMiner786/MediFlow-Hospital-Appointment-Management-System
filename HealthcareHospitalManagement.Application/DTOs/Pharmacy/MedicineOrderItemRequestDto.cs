using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Application.DTOs.Pharmacy;

public class MedicineOrderItemRequestDto
{
    public Guid     MedicineId  { get; set; }
    public int      Quantity    { get; set; }
}
