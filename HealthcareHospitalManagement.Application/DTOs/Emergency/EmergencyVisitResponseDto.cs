using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;

namespace HealthcareHospitalManagement.Application.DTOs.Emergency;

public class EmergencyVisitResponseDto
{
    public Guid     Id                  { get; set; }
    public string   EmergencyCode       { get; set; } = string.Empty;
    public string   PatientName         { get; set; } = string.Empty;
    public string   PatientCode         { get; set; } = string.Empty;
    public string?  AttendingDoctorName { get; set; }
    public string   TriageLevel         { get; set; } = string.Empty;
    public string   Status              { get; set; } = string.Empty;
    public string   ChiefComplaint      { get; set; } = string.Empty;
    public string?  ModeOfArrival       { get; set; }
    public bool     IsAmbulance         { get; set; }
    public DateTime ArrivalTime         { get; set; }
    public DateTime? TriageTime         { get; set; }
    public DateTime? DischargeTime      { get; set; }
    public string?  Diagnosis           { get; set; }
    public string?  TreatmentGiven      { get; set; }
    public bool     IsAdmitted          { get; set; }
    public bool     IsTransferred       { get; set; }
    public string?  TransferredTo       { get; set; }
    public bool     IsDeceased          { get; set; }
}
