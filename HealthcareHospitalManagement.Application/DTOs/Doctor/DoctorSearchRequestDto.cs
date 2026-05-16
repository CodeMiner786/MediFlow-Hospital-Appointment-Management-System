using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Doctor;

public class DoctorSearchRequestDto
{
    public string?               Name                    { get; set; }
    public DoctorSpecialization? Specialization          { get; set; }
    public Guid?                 DepartmentId            { get; set; }
    public bool?                 IsAvailableNow          { get; set; }
    public int                   PageNumber              { get; set; } = 1;
    public int                   PageSize                { get; set; } = 10;
}
