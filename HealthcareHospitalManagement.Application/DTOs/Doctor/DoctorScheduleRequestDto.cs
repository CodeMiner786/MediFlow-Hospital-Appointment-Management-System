using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Doctor;

public class DoctorScheduleRequestDto
{
    public Guid         DoctorId    { get; set; }
    public DayOfWeek    DayOfWeek   { get; set; }
    public TimeOnly     StartTime   { get; set; }
    public TimeOnly     EndTime     { get; set; }
    public int          SlotDurationMinutes { get; set; } = 15;
}
