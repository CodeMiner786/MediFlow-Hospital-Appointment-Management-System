namespace HealthcareHospitalManagement.Application.DTOs.Analytics;

public class ReportFilterRequestDto
{
    public DateTime DateFrom    { get; set; }
    public DateTime DateTo      { get; set; }
    public Guid?    DoctorId    { get; set; }
    public Guid?    DepartmentId { get; set; }
}
