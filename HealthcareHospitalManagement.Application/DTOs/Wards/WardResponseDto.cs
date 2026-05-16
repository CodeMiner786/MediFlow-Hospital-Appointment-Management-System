using HealthcareHospitalManagement.Domain.Enums.WardBed;

namespace HealthcareHospitalManagement.Application.DTOs.Wards;

public class WardResponseDto
{
    public Guid     Id          { get; set; }
    public string   WardName    { get; set; } = string.Empty;
    public string   WardType    { get; set; } = string.Empty;
    public int      TotalBeds   { get; set; }
    public int      AvailableBeds { get; set; }
    public int      OccupiedBeds { get; set; }
    public string   Floor       { get; set; } = string.Empty;
}
