using HealthcareHospitalManagement.Domain.Enums.Appointment;

namespace HealthcareHospitalManagement.Application.DTOs.Appointment;

public class TodayQueueItemResponseDto
{
    public Guid     AppointmentId   { get; set; }
    public string   AppointmentCode { get; set; } = string.Empty;
    public string   PatientName     { get; set; } = string.Empty;
    public string   PatientCode     { get; set; } = string.Empty;
    public TimeOnly StartTime       { get; set; }
    public string   Status          { get; set; } = string.Empty;
    public bool     IsFirstVisit    { get; set; }
    public bool     IsPaid          { get; set; }
}
