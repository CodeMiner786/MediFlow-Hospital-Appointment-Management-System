using HealthcareHospitalManagement.Domain.Enums.Lab;

namespace HealthcareHospitalManagement.Application.DTOs.Lab;

public class LabOrderItemRequestDto
{
    public Guid     LabTestId   { get; set; }
    public string?  Notes       { get; set; }
}
