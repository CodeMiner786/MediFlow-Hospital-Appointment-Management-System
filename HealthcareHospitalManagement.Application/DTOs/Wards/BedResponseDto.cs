using HealthcareHospitalManagement.Domain.Enums.WardBed;

namespace HealthcareHospitalManagement.Application.DTOs.Wards;

public class BedResponseDto
{
    public Guid     Id          { get; set; }
    public string   BedNumber   { get; set; } = string.Empty;
    public string   BedType     { get; set; } = string.Empty;
    public string   Status      { get; set; } = string.Empty;
    public string   WardName    { get; set; } = string.Empty;
    public decimal  DailyCharge { get; set; }
    public bool     IsAvailable { get; set; }
}
