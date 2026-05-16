using HealthcareHospitalManagement.Domain.Enums.WardBed;

namespace HealthcareHospitalManagement.Application.DTOs.Wards;

public class BedFilterRequestDto
{
    public Guid?        WardId      { get; set; }
    public BedStatus?   Status      { get; set; }
    public WardType?    WardType    { get; set; }
    public int          PageNumber  { get; set; } = 1;
    public int          PageSize    { get; set; } = 10;
}
