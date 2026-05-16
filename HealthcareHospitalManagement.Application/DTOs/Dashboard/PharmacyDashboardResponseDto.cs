namespace HealthcareHospitalManagement.Application.DTOs.Dashboard;

public class PharmacyDashboardResponseDto
{
    public int      PendingOrders           { get; set; }
    public int      ProcessingOrders        { get; set; }
    public int      CompletedToday          { get; set; }
    public int      TotalOrdersThisMonth    { get; set; }
    public decimal  TodayRevenue            { get; set; }
    public decimal  MonthlyRevenue          { get; set; }
    public decimal  WalletBalance           { get; set; }
    public int      LowStockMedicines       { get; set; }
    public int      OutOfStockMedicines     { get; set; }
    public int      ExpiringMedicines       { get; set; }
}
