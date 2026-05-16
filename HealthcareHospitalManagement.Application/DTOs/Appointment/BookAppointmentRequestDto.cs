using HealthcareHospitalManagement.Domain.Enums.Appointment;

namespace HealthcareHospitalManagement.Application.DTOs.Appointment;

public class BookAppointmentRequestDto
{
    public Guid             PatientId           { get; set; }
    public Guid             DoctorId            { get; set; }
    public DateTime         AppointmentDate     { get; set; }
    public TimeOnly         StartTime           { get; set; }
    public AppointmentType  AppointmentType     { get; set; }
    public string           ReasonForVisit      { get; set; } = string.Empty;
    public string?          Symptoms            { get; set; }
    public bool             IsFollowUp          { get; set; }
    public Guid?            PreviousAppointmentId { get; set; }
}
