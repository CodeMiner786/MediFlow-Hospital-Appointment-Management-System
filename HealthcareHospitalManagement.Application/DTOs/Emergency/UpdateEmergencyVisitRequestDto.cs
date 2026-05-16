using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;

namespace HealthcareHospitalManagement.Application.DTOs.Emergency;

public class UpdateEmergencyVisitRequestDto
{
    public Guid     EmergencyVisitId    { get; set; }
    public string?  Diagnosis           { get; set; }
    public string?  TreatmentGiven      { get; set; }
    public string?  Medications         { get; set; }
    public string?  DischargeNotes      { get; set; }
    public bool     IsAdmitted          { get; set; }
    public Guid?    AdmissionId         { get; set; }
    public bool     IsTransferred       { get; set; }
    public string?  TransferredTo       { get; set; }
    public bool     IsDeceased          { get; set; }
    public string?  DeathCause          { get; set; }
}
