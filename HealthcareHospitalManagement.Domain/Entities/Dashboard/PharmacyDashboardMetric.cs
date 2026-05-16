using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;

namespace HealthcareHospitalManagement.Domain.Entities.Dashboard;

public class PharmacyDashboardMetric : BaseEntity
{
    // ── ১. ফার্মেসি প্রোফাইল এবং টাইম স্ট্যাম্প ──────────────────────────────────────
    public    Guid              PharmacyProfileId       { get; set; }
    public    PharmacyProfile   PharmacyProfile         { get; set; } = null!;
    public    DateOnly          MetricDate              { get; set; }

    // ── ২. অর্ডার সামারি (Order Stats) ─────────────────────────────────────────────
    public    int               TotalOrdersToday        { get; set; }
    public    int               CompletedOrders         { get; set; }
    public    int               PendingOrders           { get; set; }
    public    int               OutForDeliveryOrders    { get; set; }
    public    int               CancelledOrders         { get; set; }

    // ── ৩. ইনভেন্টরি বা স্টক (Stock Stats) ─────────────────────────────────────────
    public    int               TotalMedicines          { get; set; }
    public    int               LowStockMedicines       { get; set; }
    public    int               OutOfStockMedicines     { get; set; }
    public    int               ExpiringThisMonth       { get; set; }

    // ── ৪. আয়-রোজগার (Financials) ─────────────────────────────────────────────────
    public    decimal           TodayRevenue            { get; set; }
    public    decimal           MonthlyRevenue          { get; set; }
    public    decimal           PendingPayments         { get; set; }

    // ── ৫. রেটিং এবং রিভিউ (Rating Stats) ──────────────────────────────────────────
    public    decimal           AverageRating           { get; set; }
    public    int               TotalRatings            { get; set; }
}