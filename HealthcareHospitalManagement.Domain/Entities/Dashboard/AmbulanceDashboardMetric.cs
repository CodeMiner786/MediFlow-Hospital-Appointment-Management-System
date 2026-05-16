using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Ambulance;

namespace HealthcareHospitalManagement.Domain.Entities.Dashboard;

public class AmbulanceDashboardMetric : BaseEntity
{
    // ── ১. প্রোভাইডার এবং টাইম স্ট্যাম্প ──────────────────────────────────────────────
    public    Guid                      ProviderId              { get; set; }
    public    AmbulanceProviderProfile  Provider                { get; set; } = null!;
    public    DateOnly                  MetricDate              { get; set; }

    // ── ২. বুকিং স্ট্যাটাস (Booking Stats) ──────────────────────────────────────────
    public    int                       TotalBookingsToday      { get; set; }
    public    int                       CompletedBookings       { get; set; }
    public    int                       PendingBookings         { get; set; }
    public    int                       CancelledBookings       { get; set; }

    // ── ৩. গাড়ির বহর (Fleet Stats) ────────────────────────────────────────────────
    public    int                       TotalVehicles           { get; set; }
    public    int                       AvailableVehicles       { get; set; }
    public    int                       VehiclesOnDuty          { get; set; }
    public    int                       VehiclesUnderMaintenance { get; set; }

    // ── ৪. ওয়ালেট এবং আয় (Financials) ───────────────────────────────────────────────
    public    decimal                   TodayEarnings           { get; set; }
    public    decimal                   MonthlyEarnings         { get; set; }
    public    decimal                   WalletBalance           { get; set; }
    public    decimal                   PendingPayments         { get; set; }

    // ── ৫. রেটিং (Rating Stats) ────────────────────────────────────────────────────
    public    decimal                   AverageRating           { get; set; }
    public    int                       TotalRatings            { get; set; }
}