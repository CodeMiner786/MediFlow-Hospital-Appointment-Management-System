using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Doctor;

public class DoctorSummaryResponseDto
{
    public Guid     Id                  { get; set; }
    public string   FullName            { get; set; } = string.Empty;
    public string   Specialization      { get; set; } = string.Empty;
    public string   DepartmentName      { get; set; } = string.Empty;
    public decimal  ConsultationFee     { get; set; }
    public decimal  AverageRating       { get; set; }
    public int      TotalReviews        { get; set; }
    public string   AvailabilityStatus  { get; set; } = string.Empty;
    public string?  ProfileImageUrl     { get; set; }
}
