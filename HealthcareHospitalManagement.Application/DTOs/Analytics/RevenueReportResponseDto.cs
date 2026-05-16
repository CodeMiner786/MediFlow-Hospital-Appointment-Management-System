namespace HealthcareHospitalManagement.Application.DTOs.Analytics;


public class RevenueReportResponseDto
{
    public DateTime DateFrom        { get; set; }
    public DateTime DateTo          { get; set; }
    public decimal  TotalRevenue    { get; set; }
    public List<DailyRevenueDto> DailyBreakdown { get; set; } = [];
}
