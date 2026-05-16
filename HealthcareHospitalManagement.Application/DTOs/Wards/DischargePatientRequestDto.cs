using HealthcareHospitalManagement.Domain.Enums.WardBed;

namespace HealthcareHospitalManagement.Application.DTOs.Wards;

public class DischargePatientRequestDto
{
    public Guid     AdmissionId         { get; set; }
    public DateTime DischargeDate       { get; set; }
    public string?  DischargeNotes      { get; set; }
    public string?  DischargeSummary    { get; set; }
    public bool     IsTransferred       { get; set; }
    public string?  TransferredTo       { get; set; }
    public string?  TransferReason      { get; set; }
}
