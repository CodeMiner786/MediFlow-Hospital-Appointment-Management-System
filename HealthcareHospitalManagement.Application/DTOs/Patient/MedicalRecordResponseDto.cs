using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Patient;

public class MedicalRecordResponseDto
{
    public Guid     Id              { get; set; }
    public string   DoctorName      { get; set; } = string.Empty;
    public string?  Diagnosis       { get; set; }
    public string?  TreatmentPlan   { get; set; }
    public string?  Notes           { get; set; }
    public DateTime VisitDate       { get; set; }
}
