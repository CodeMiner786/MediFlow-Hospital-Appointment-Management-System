using HealthcareHospitalManagement.Domain.Enums.Telemedicine;

namespace HealthcareHospitalManagement.Application.DTOs.Telemedicine;

public class EndSessionRequestDto
{
    public Guid     SessionId           { get; set; }
    public string?  DoctorNotes         { get; set; }
    public string?  Prescription        { get; set; }
    public string?  FollowUpInstructions { get; set; }
    public DateTime? FollowUpDate        { get; set; }
}
