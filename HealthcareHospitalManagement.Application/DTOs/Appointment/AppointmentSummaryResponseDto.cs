using HealthcareHospitalManagement.Domain.Enums.Appointment;

namespace HealthcareHospitalManagement.Application.DTOs.Appointment;

public class AppointmentSummaryResponseDto
{
    public Guid     Id                  { get; set; }
    public string   AppointmentCode     { get; set; } = string.Empty;
    public string   PatientName         { get; set; } = string.Empty;
    public string   DoctorName          { get; set; } = string.Empty;
    public DateTime AppointmentDate     { get; set; }
    public TimeOnly StartTime           { get; set; }
    public TimeOnly EndTime             { get; set; }
    public string   AppointmentType     { get; set; } = string.Empty;
    public string   Status              { get; set; } = string.Empty;
    public bool     IsPaid              { get; set; }
}
