using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Ambulance;

public class AmbulanceBookingResponseDto
{
    public Guid     Id                      { get; set; }
    public string   BookingCode             { get; set; } = string.Empty;
    public string   VehicleNumber           { get; set; } = string.Empty;
    public string   ProviderName            { get; set; } = string.Empty;
    public string   RequesterName           { get; set; } = string.Empty;
    public string   RequesterPhone          { get; set; } = string.Empty;
    public string   Status                  { get; set; } = string.Empty;
    public DateTime RequestedAt             { get; set; }
    public DateTime? DispatchedAt           { get; set; }
    public DateTime? ArrivedAt              { get; set; }
    public DateTime? CompletedAt            { get; set; }
    public string   PickupAddress           { get; set; } = string.Empty;
    public string?  DropAddress             { get; set; }
    public decimal? Fare                    { get; set; }
    public bool     IsPaid                  { get; set; }
}
