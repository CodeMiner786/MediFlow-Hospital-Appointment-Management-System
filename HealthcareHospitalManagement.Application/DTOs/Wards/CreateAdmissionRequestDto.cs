using HealthcareHospitalManagement.Domain.Enums.WardBed;

namespace HealthcareHospitalManagement.Application.DTOs.Wards;

public class CreateAdmissionRequestDto
{
    public Guid     PatientId           { get; set; }
    public Guid     BedId               { get; set; }
    public Guid     AdmittingDoctorId   { get; set; }
    public DateTime AdmissionDate       { get; set; }
    public string   AdmissionReason     { get; set; } = string.Empty;
    public string?  Diagnosis           { get; set; }
    public bool     IsTransferred       { get; set; }
    public string?  TransferredFrom     { get; set; }
}
