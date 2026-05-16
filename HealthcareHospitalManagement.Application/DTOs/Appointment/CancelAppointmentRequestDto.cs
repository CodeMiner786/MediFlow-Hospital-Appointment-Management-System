using HealthcareHospitalManagement.Domain.Enums.Appointment;


namespace HealthcareHospitalManagement.Application.DTOs.Appointment;

public class CancelAppointmentRequestDto
{
    public Guid     AppointmentId       { get; set; }
    public string   CancellationReason  { get; set; } = string.Empty;
}
