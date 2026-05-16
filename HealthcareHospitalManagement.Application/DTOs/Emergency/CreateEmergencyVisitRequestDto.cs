using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;

namespace HealthcareHospitalManagement.Application.DTOs.Emergency;

public class CreateEmergencyVisitRequestDto
{
    public Guid             PatientId               { get; set; }
    public Guid?            AttendingDoctorId        { get; set; }
    public Guid?            AmbulanceBookingId       { get; set; }
    public DateTime         ArrivalTime             { get; set; } = DateTime.UtcNow;
    public EmergencyLevel   TriageLevel             { get; set; }
    public string           ChiefComplaint          { get; set; } = string.Empty;
    public string?          ModeOfArrival           { get; set; }
    public bool             IsAmbulance             { get; set; }
    public string?          InitialAssessment       { get; set; }
}
