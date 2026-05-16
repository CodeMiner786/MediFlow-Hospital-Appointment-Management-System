using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Report;

namespace HealthcareHospitalManagement.Domain.Entities.Dashboard;

public class AdminDashboardMetric : BaseEntity
{
    // ── ১. টাইম স্ট্যাম্প ──────────────────────────────────────────────────────────
    public    DateOnly      MetricDate                  { get; set; }

    // ── ২. ইউজার স্ট্যাটাস (User Stats) ───────────────────────────────────────────
    public    int           TotalRegisteredUsers        { get; set; }
    public    int           NewUsersToday               { get; set; }
    public    int           ActiveDoctors               { get; set; }
    public    int           ActiveAmbulanceProviders    { get; set; }
    public    int           ActiveLabProfiles           { get; set; }
    public    int           ActivePharmacies            { get; set; }
    public    int           PendingVerifications        { get; set; }

    // ── ৩. বুকিং সামারি (Booking Summary) ────────────────────────────────────────
    public    int           TotalAppointmentsToday      { get; set; }
    public    int           TotalAmbulanceBookingsToday { get; set; }
    public    int           TotalLabOrdersToday         { get; set; }
    public    int           TotalMedicineOrdersToday    { get; set; }
    public    int           TotalBedBookingsToday       { get; set; }

    // ── ৪. রেভিনিউ (Financials) ─────────────────────────────────────────────────
    public    decimal       PlatformRevenueToday        { get; set; }
    public    decimal       DoctorRevenueToday          { get; set; }
    public    decimal       LabRevenueToday             { get; set; }
    public    decimal       PharmacyRevenueToday        { get; set; }
    public    decimal       BedBookingRevenueToday      { get; set; }
    public    decimal       MonthlyPlatformRevenue      { get; set; }
    public    decimal       TotalAmbulanceWalletBalance { get; set; }

    // ── ৫. অ্যাক্টিভিটি এবং মেটাডাটা ───────────────────────────────────────────────
    public    int           TotalAuditLogsToday         { get; set; }
    public    int           ActiveConversationsToday    { get; set; }
    public    string?       LastReportGeneratedAt       { get; set; }
    public    ReportStatus  LastReportStatus            { get; set; } = ReportStatus.Draft;
}