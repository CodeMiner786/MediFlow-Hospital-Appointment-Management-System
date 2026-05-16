namespace HealthcareHospitalManagement.Application.DTOs.Dashboard;

public class PatientDashboardResponseDto
{
    public int      UpcomingAppointments    { get; set; }
    public int      TotalAppointments       { get; set; }
    public int      PendingLabOrders        { get; set; }
    public int      ActivePrescriptions     { get; set; }
    public int      UnreadNotifications     { get; set; }
    public decimal  OutstandingDues         { get; set; }
    public bool     HasInsurance            { get; set; }
    public string?  LastVisitDoctorName     { get; set; }
    public DateTime? LastVisitDate          { get; set; }
    public string?  NextAppointmentDoctor   { get; set; }
    public DateTime? NextAppointmentDate    { get; set; }
}
