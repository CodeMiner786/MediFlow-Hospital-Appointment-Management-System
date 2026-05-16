namespace HealthcareHospitalManagement.Application.DTOs.Dashboard;

public class AmbulanceDashboardResponseDto
{
    public int      TotalVehicles           { get; set; }
    public int      AvailableVehicles       { get; set; }
    public int      ActiveBookings          { get; set; }
    public int      TodayBookings           { get; set; }
    public int      CompletedToday          { get; set; }
    public decimal  TodayRevenue            { get; set; }
    public decimal  MonthlyRevenue          { get; set; }
    public decimal  WalletBalance           { get; set; }
    public decimal  AverageRating           { get; set; }
}
