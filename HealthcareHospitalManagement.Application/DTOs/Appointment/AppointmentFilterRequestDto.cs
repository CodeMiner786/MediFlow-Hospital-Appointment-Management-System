using HealthcareHospitalManagement.Domain.Enums.Appointment;


namespace HealthcareHospitalManagement.Application.DTOs.Appointment;

public class AppointmentFilterRequestDto
{
    public Guid?                PatientId   { get; set; }
    public Guid?                DoctorId    { get; set; }
    public AppointmentStatus?   Status      { get; set; }
    public DateTime?            DateFrom    { get; set; }
    public DateTime?            DateTo      { get; set; }
    public int                  PageNumber  { get; set; } = 1;
    public int                  PageSize    { get; set; } = 10;
}
