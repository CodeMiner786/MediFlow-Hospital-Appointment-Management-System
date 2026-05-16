using HealthcareHospitalManagement.Domain.Enums.Lab;

namespace HealthcareHospitalManagement.Application.DTOs.Lab;

public class LabOrderResponseDto
{
    public Guid     Id                  { get; set; }
    public string   OrderCode           { get; set; } = string.Empty;
    public string   PatientName         { get; set; } = string.Empty;
    public string?  OrderedByDoctorName { get; set; }
    public string   LabName             { get; set; } = string.Empty;
    public string   Priority            { get; set; } = string.Empty;
    public DateTime OrderDate           { get; set; }
    public bool     IsPaid              { get; set; }
    public decimal  TotalAmount         { get; set; }
    public List<LabOrderItemResponseDto> Items { get; set; } = [];
}
