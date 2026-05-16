using HealthcareHospitalManagement.Domain.Enums.Lab;

namespace HealthcareHospitalManagement.Application.DTOs.Lab;

public class LabOrderItemResponseDto
{
    public Guid     Id          { get; set; }
    public string   TestName    { get; set; } = string.Empty;
    public string   TestCode    { get; set; } = string.Empty;
    public string   Status      { get; set; } = string.Empty;
    public string?  Result      { get; set; }
    public DateTime? CompletedAt { get; set; }
}
