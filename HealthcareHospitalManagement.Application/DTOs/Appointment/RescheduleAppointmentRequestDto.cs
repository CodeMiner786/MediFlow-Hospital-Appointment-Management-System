using HealthcareHospitalManagement.Domain.Enums.Appointment;

namespace HealthcareHospitalManagement.Application.DTOs.Appointment;

public class RescheduleAppointmentRequestDto
{
    public Guid     AppointmentId   { get; set; }
    public DateTime NewDate         { get; set; }
    public TimeOnly NewStartTime    { get; set; }
}
