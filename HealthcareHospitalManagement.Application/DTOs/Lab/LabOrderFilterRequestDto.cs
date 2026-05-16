using HealthcareHospitalManagement.Domain.Enums.Lab;

namespace HealthcareHospitalManagement.Application.DTOs.Lab;

public class LabOrderFilterRequestDto
{
    public Guid?        PatientId   { get; set; }
    public Guid?        DoctorId    { get; set; }
    public TestPriority? Priority   { get; set; }
    public bool?        IsPaid      { get; set; }
    public DateTime?    DateFrom    { get; set; }
    public DateTime?    DateTo      { get; set; }
    public int          PageNumber  { get; set; } = 1;
    public int          PageSize    { get; set; } = 10;
}
