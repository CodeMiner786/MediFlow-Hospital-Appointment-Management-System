using HealthcareHospitalManagement.Domain.Enums.Telemedicine;

namespace HealthcareHospitalManagement.Application.DTOs.Telemedicine;

public class CreateTelemedicineSessionRequestDto
{
    public Guid             AppointmentId   { get; set; }
    public Guid             PatientId       { get; set; }
    public Guid             DoctorId        { get; set; }
    public VideoCallProvider Provider       { get; set; }
    public DateTime         ScheduledAt     { get; set; }
}
