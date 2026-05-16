using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;

namespace HealthcareHospitalManagement.Application.DTOs.Emergency;

public class EmergencyVisitFilterRequestDto
{
    public EmergencyLevel?  TriageLevel { get; set; }
    public EmergencyStatus? Status      { get; set; }
    public DateTime?        DateFrom    { get; set; }
    public DateTime?        DateTo      { get; set; }
    public int              PageNumber  { get; set; } = 1;
    public int              PageSize    { get; set; } = 10;
}
