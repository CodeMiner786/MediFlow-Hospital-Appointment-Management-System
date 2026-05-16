using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Doctor;

public class DoctorDetailResponseDto : DoctorSummaryResponseDto
{
    public string   DoctorCode          { get; set; } = string.Empty;
    public string   LicenseNumber       { get; set; } = string.Empty;
    public int      ExperienceYears     { get; set; }
    public string   Qualifications      { get; set; } = string.Empty;
    public string?  Biography           { get; set; }
    public string   PhoneNumber         { get; set; } = string.Empty;
    public string   Email               { get; set; } = string.Empty;
    public decimal? TelemedicineConsultationFee { get; set; }
    public decimal? HomeVisitFee        { get; set; }
    public string?  SubSpecialization   { get; set; }
}
