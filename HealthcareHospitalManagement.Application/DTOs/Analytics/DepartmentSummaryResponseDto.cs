namespace HealthcareHospitalManagement.Application.DTOs.Analytics;

public class DepartmentSummaryResponseDto
{
    public Guid     DepartmentId        { get; set; }
    public string   DepartmentName      { get; set; } = string.Empty;
    public int      TotalDoctors        { get; set; }
    public int      TotalAppointments   { get; set; }
    public int      TotalPatients       { get; set; }
    public decimal  TotalRevenue        { get; set; }
}
