using HealthcareHospitalManagement.Domain.Enums.Lab;

namespace HealthcareHospitalManagement.Application.DTOs.Lab;

public class LabTestResponseDto
{
    public Guid     Id          { get; set; }
    public string   TestName    { get; set; } = string.Empty;
    public string   TestCode    { get; set; } = string.Empty;
    public string   Category    { get; set; } = string.Empty;
    public decimal  Price       { get; set; }
    public string?  Description { get; set; }
    public string?  SampleType  { get; set; }
    public int      TurnAroundHours { get; set; }
}
