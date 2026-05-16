using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Ambulance;

public class BookAmbulanceRequestDto
{
    public Guid     VehicleId               { get; set; }
    public Guid     ProviderId              { get; set; }
    public Guid?    PatientId               { get; set; }
    public string   RequesterName           { get; set; } = string.Empty;
    public string   RequesterPhone          { get; set; } = string.Empty;
    public string   PickupAddress           { get; set; } = string.Empty;
    public double?  PickupLatitude          { get; set; }
    public double?  PickupLongitude         { get; set; }
    public string?  DropAddress             { get; set; }
    public double?  DropLatitude            { get; set; }
    public double?  DropLongitude           { get; set; }
    public string?  EmergencyDescription    { get; set; }
}
