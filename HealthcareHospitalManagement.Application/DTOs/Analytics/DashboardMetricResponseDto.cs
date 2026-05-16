namespace HealthcareHospitalManagement.Application.DTOs.Analytics;

public class DashboardMetricResponseDto
{
    public DateOnly MetricDate              { get; set; }

    // Patients
    public int      TotalPatients           { get; set; }
    public int      NewPatients             { get; set; }
    public int      OpdVisits               { get; set; }
    public int      IpdAdmissions           { get; set; }
    public int      Discharges              { get; set; }

    // Appointments
    public int      TotalAppointments       { get; set; }
    public int      CompletedAppointments   { get; set; }
    public int      CancelledAppointments   { get; set; }
    public int      TelemedicineSessions    { get; set; }

    // Revenue
    public decimal  TotalRevenue            { get; set; }
    public decimal  DoctorRevenue           { get; set; }
    public decimal  PharmacyRevenue         { get; set; }
    public decimal  LabRevenue              { get; set; }
    public decimal  AmbulanceRevenue        { get; set; }
    public decimal  BedBookingRevenue       { get; set; }
    public decimal  PendingDues             { get; set; }

    // Operational
    public int      AmbulanceBookings       { get; set; }
    public int      AmbulanceAvailable      { get; set; }
    public int      LabTestsOrdered         { get; set; }
    public int      LabTestsCompleted       { get; set; }

    // Active Entities
    public int      ActiveDoctors           { get; set; }
    public int      ActiveAmbulanceProviders { get; set; }
    public int      ActiveLabProfiles       { get; set; }
    public int      ActivePharmacies        { get; set; }
    public int      TotalRegisteredUsers    { get; set; }
}
