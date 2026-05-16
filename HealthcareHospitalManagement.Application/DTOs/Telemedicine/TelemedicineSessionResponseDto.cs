using HealthcareHospitalManagement.Domain.Enums.Telemedicine;

namespace HealthcareHospitalManagement.Application.DTOs.Telemedicine;

public class TelemedicineSessionResponseDto
{
    public Guid     Id                  { get; set; }
    public string   SessionCode         { get; set; } = string.Empty;
    public string   PatientName         { get; set; } = string.Empty;
    public string   DoctorName          { get; set; } = string.Empty;
    public string   Provider            { get; set; } = string.Empty;
    public string   Status              { get; set; } = string.Empty;
    public string?  MeetingLink         { get; set; }
    public string?  PatientToken        { get; set; }
    public string?  DoctorToken         { get; set; }
    public DateTime ScheduledAt         { get; set; }
    public DateTime? StartedAt          { get; set; }
    public DateTime? EndedAt            { get; set; }
    public int?     DurationMinutes     { get; set; }
    public string?  DoctorNotes         { get; set; }
    public string?  FollowUpInstructions { get; set; }
    public DateTime? FollowUpDate        { get; set; }
    public bool     IsRecorded          { get; set; }
}
