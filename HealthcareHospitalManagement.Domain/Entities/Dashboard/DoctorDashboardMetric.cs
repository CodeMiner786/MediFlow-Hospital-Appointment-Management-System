using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Doctor;

namespace HealthcareHospitalManagement.Domain.Entities.Dashboard;

public class DoctorDashboardMetric : BaseEntity
{
    // ── ১. ডাক্তার এবং টাইম স্ট্যাম্প ──────────────────────────────────────────────
    public    Guid            DoctorId                  { get; set; }
    public    DoctorEntity    Doctor                    { get; set; } = null!;
    public    DateOnly        MetricDate                { get; set; }

    // ── ২. অ্যাপয়েন্টমেন্ট সামারি (Appointment Stats) ─────────────────────────────
    public    int             TotalAppointmentsToday    { get; set; }
    public    int             CompletedAppointments     { get; set; }
    public    int             PendingAppointments       { get; set; }
    public    int             CancelledAppointments     { get; set; }
    public    int             NoShowAppointments        { get; set; }
    public    int             TelemedicineAppointments  { get; set; }

    // ── ৩. পেশেন্ট সামারি (Patient Stats) ──────────────────────────────────────────
    public    int             TotalPatientsToday        { get; set; }
    public    int             NewPatientsToday          { get; set; }
    public    int             FollowUpPatients          { get; set; }

    // ── ৪. আয়-রোজগার (Financials) ────────────────────────────────────────────────
    public    decimal         TotalRevenueToday         { get; set; }
    public    decimal         ConsultationRevenue       { get; set; }
    public    decimal         TelemedicineRevenue       { get; set; }
    public    decimal         PendingPayments           { get; set; }
    public    int             MonthlyAppointments       { get; set; }
    public    decimal         MonthlyRevenue            { get; set; }

    // ── ৫. রেটিং এবং রিভিউ (Rating Stats) ─────────────────────────────────────────
    public    decimal         AverageRating             { get; set; }
    public    int             TotalReviews              { get; set; }
}