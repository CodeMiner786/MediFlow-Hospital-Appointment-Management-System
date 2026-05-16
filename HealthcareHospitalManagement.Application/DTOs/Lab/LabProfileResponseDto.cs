using HealthcareHospitalManagement.Domain.Enums.Lab;

namespace HealthcareHospitalManagement.Application.DTOs.Lab;

public class LabProfileResponseDto
{
    public Guid     Id              { get; set; }
    public string   LabName         { get; set; } = string.Empty;
    public string   LicenseNumber   { get; set; } = string.Empty;
    public string   ContactNumber   { get; set; } = string.Empty;
    public string   Address         { get; set; } = string.Empty;
    public bool     IsVerified      { get; set; }
    public decimal  AverageRating   { get; set; }
    public bool     IsHomeCollectionAvailable { get; set; }
}
