using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Doctor;

public class DepartmentResponseDto
{
    public Guid     Id          { get; set; }
    public string   Name        { get; set; } = string.Empty;
    public string?  Description { get; set; }
    public int      DoctorCount { get; set; }
}
