namespace HealthcareHospitalManagement.Application.DTOs.Feedback;

public class CreateFeedbackRequestDto
{
    public Guid     ReviewerUserId  { get; set; }
    public Guid     TargetId        { get; set; }     // DoctorId / PharmacyId / LabId etc.
    public string   TargetType      { get; set; } = string.Empty;
    public int      Rating          { get; set; }     // 1–5
    public string?  Comment         { get; set; }
    public Guid?    AppointmentId   { get; set; }
}
