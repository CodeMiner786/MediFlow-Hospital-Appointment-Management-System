using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Lab;

namespace HealthcareHospitalManagement.Domain.Entities.Dashboard;

public class LabDashboardMetric : BaseEntity
{
    // ── ১. ল্যাব প্রোফাইল এবং টাইম স্ট্যাম্প ──────────────────────────────────────────
    public    Guid            LabProfileId              { get; set; }
    public    LabProfile      LabProfile                { get; set; } = null!;
    public    DateOnly        MetricDate                { get; set; }

    // ── ২. ল্যাব অর্ডার সামারি (Order Stats) ────────────────────────────────────────
    public    int             TotalOrdersToday          { get; set; }
    public    int             CompletedOrders           { get; set; }
    public    int             PendingOrders             { get; set; }
    public    int             SampleCollectedOrders     { get; set; }
    public    int             ReportDeliveredOrders     { get; set; }

    // ── ৩. টেস্ট ক্যাটালগ (Test Stats) ─────────────────────────────────────────────
    public    int             TotalTestsOffered         { get; set; }
    public    int             ActiveTests               { get; set; }

    // ── ৪. আয়-রোজগার (Financials) ────────────────────────────────────────────────
    public    decimal         TodayRevenue              { get; set; }
    public    decimal         MonthlyRevenue            { get; set; }
    public    decimal         PendingPayments           { get; set; }

    // ── ৫. রেটিং এবং রিভিউ (Rating Stats) ─────────────────────────────────────────
    public    decimal         AverageRating             { get; set; }
    public    int             TotalRatings              { get; set; }
}