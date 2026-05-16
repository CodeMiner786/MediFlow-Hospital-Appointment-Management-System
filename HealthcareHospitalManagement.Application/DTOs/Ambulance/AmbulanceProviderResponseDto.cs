using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using HealthcareHospitalManagement.Domain.Enums.BillingPayment;

namespace HealthcareHospitalManagement.Application.DTOs.Ambulance;

public class AmbulanceProviderResponseDto
{
    public Guid     Id                  { get; set; }
    public string   ProviderName        { get; set; } = string.Empty;
    public string   ContactNumber       { get; set; } = string.Empty;
    public string   EmergencyHotline    { get; set; } = string.Empty;
    public string   Address             { get; set; } = string.Empty;
    public string   City                { get; set; } = string.Empty;
    public bool     IsVerified          { get; set; }
    public decimal  AverageRating       { get; set; }
    public int      AvailableVehicles   { get; set; }
    public string?  LogoUrl             { get; set; }
}
