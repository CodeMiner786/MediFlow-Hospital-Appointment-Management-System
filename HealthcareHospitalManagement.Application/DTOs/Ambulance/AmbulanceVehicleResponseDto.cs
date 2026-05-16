using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Ambulance;

public class AmbulanceVehicleResponseDto
{
    public Guid     Id                  { get; set; }
    public string   RegistrationNumber  { get; set; } = string.Empty;
    public string   VehicleType         { get; set; } = string.Empty;
    public string   Status              { get; set; } = string.Empty;
    public bool     IsAvailable         { get; set; }
    public string?  DriverName          { get; set; }
    public string?  DriverPhone         { get; set; }
}
