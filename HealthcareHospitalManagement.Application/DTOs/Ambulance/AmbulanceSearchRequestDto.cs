using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Ambulance;

public class AmbulanceSearchRequestDto
{
    public string?  City        { get; set; }
    public string?  District    { get; set; }
    public double?  Latitude    { get; set; }
    public double?  Longitude   { get; set; }
    public int      PageNumber  { get; set; } = 1;
    public int      PageSize    { get; set; } = 10;
}
