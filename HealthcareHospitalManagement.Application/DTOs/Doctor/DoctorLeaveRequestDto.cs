using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Doctor;

public class DoctorLeaveRequestDto
{
    public Guid     DoctorId    { get; set; }
    public DateTime LeaveDate   { get; set; }
    public string?  Reason      { get; set; }
}
