using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using HealthcareHospitalManagement.Domain.Enums.BillingPayment;


namespace HealthcareHospitalManagement.Application.DTOs.Ambulance;


public class UpdateAmbulanceBookingStatusRequestDto
{
    public Guid                     BookingId   { get; set; }
    public AmbulanceBookingStatus   Status      { get; set; }
    public decimal?                 Fare        { get; set; }
}
