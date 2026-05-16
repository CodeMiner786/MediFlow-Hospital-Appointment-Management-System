namespace HealthcareHospitalManagement.Application.DTOs.Dashboard;

public class LabDashboardResponseDto
{
    public int      PendingOrders           { get; set; }
    public int      InProgressOrders        { get; set; }
    public int      CompletedToday          { get; set; }
    public int      TotalOrdersThisMonth    { get; set; }
    public decimal  TodayRevenue            { get; set; }
    public decimal  MonthlyRevenue          { get; set; }
    public decimal  WalletBalance           { get; set; }
    public int      UnpaidOrders            { get; set; }
}
