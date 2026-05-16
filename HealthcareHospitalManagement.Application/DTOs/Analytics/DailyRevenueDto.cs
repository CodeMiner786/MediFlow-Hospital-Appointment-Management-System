namespace HealthcareHospitalManagement.Application.DTOs.Analytics;

public class DailyRevenueDto
{
    public DateTime Date            { get; set; }
    public decimal  Revenue         { get; set; }
    public int      TransactionCount { get; set; }
}
