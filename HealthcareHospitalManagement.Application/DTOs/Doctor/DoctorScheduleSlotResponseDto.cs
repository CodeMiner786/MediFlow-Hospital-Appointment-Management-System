using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Doctor;

public class DoctorScheduleSlotResponseDto
{
    public Guid     SlotId      { get; set; }
    public TimeOnly StartTime   { get; set; }
    public TimeOnly EndTime     { get; set; }
    public bool     IsBooked    { get; set; }
    public bool     IsAvailable { get; set; }
}
