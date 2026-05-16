namespace HealthcareHospitalManagement.Application.DTOs.Analytics;

public class DoctorPerformanceResponseDto
{
    public Guid     DoctorId            { get; set; }
    public string   DoctorName          { get; set; } = string.Empty;
    public string   Specialization      { get; set; } = string.Empty;
    public int      TotalAppointments   { get; set; }
    public int      CompletedCount      { get; set; }
    public int      CancelledCount      { get; set; }
    public decimal  AverageRating       { get; set; }
    public decimal  TotalEarnings       { get; set; }
    public DateTime ReportPeriodFrom    { get; set; }
    public DateTime ReportPeriodTo      { get; set; }
}
