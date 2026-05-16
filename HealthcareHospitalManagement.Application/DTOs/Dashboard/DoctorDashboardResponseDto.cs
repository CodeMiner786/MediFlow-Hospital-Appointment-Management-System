namespace HealthcareHospitalManagement.Application.DTOs.Dashboard;

public class DoctorDashboardResponseDto
{
    public int      TodayAppointments       { get; set; }
    public int      PendingAppointments     { get; set; }
    public int      CompletedToday          { get; set; }
    public int      CancelledToday          { get; set; }
    public int      TotalPatients           { get; set; }
    public decimal  TodayEarnings           { get; set; }
    public decimal  MonthlyEarnings         { get; set; }
    public decimal  AverageRating           { get; set; }
    public int      TotalReviews            { get; set; }
    public bool     IsAvailable             { get; set; }
    public int      ActiveTelemedicineSessions { get; set; }
}
