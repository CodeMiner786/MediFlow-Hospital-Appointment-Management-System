namespace HealthcareHospitalManagement.Application.DTOs.Dashboard;

public class AdminDashboardResponseDto
{
    public int      TotalDoctors            { get; set; }
    public int      TotalPatients           { get; set; }
    public int      TodayAppointments       { get; set; }
    public int      TodayRevenue            { get; set; }
    public decimal  MonthlyRevenue          { get; set; }
    public int      PendingVerifications    { get; set; }
    public int      ActiveAdmissions        { get; set; }
    public int      AvailableBeds           { get; set; }
    public int      ActiveAmbulances        { get; set; }
    public int      PendingLabOrders        { get; set; }
    public int      ActivePharmacyOrders    { get; set; }
    public int      UnresolvedEmergencies   { get; set; }
}
