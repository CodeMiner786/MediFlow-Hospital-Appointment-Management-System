using HealthcareHospitalManagement.Domain.Enums.WardBed;

namespace HealthcareHospitalManagement.Application.DTOs.Wards;

public class AdmissionResponseDto
{
    public Guid     Id                  { get; set; }
    public string   AdmissionCode       { get; set; } = string.Empty;
    public string   PatientName         { get; set; } = string.Empty;
    public string   PatientCode         { get; set; } = string.Empty;
    public string   BedNumber           { get; set; } = string.Empty;
    public string   WardName            { get; set; } = string.Empty;
    public string   AdmittingDoctorName { get; set; } = string.Empty;
    public DateTime AdmissionDate       { get; set; }
    public DateTime? DischargeDate      { get; set; }
    public string   Status              { get; set; } = string.Empty;
    public string   AdmissionReason     { get; set; } = string.Empty;
    public string?  Diagnosis           { get; set; }
    public bool     IsTransferred       { get; set; }
}
