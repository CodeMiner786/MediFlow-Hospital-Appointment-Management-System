using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Report;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class DoctorPerformanceReport : BaseEntity
{
    // ——— ১. আইডেন্টিটি ও পিরিয়ড ———
    public          Guid       DoctorId             { get; set; }
    public          DoctorEntity Doctor             { get; set; } = null!;
    public          DateTime   FromDate             { get; set; }
    public          DateTime   ToDate               { get; set; }
    public          ReportPeriodType PeriodType { get; set; } = ReportPeriodType.Monthly;

    // ——— ২. অ্যাপয়েন্টমেন্ট স্ট্যাটিস্টিকস ———
    public          int        TotalAppointments      { get; set; }
    public          int        CompletedAppointments  { get; set; }
    public          int        CancelledAppointments  { get; set; }
    public          int        NoShows                { get; set; }
    public          decimal    CompletionRate         { get; set; }

    // ——— ৩. পেশেন্ট ও ভিজিট ডেটা ———
    public          int        TotalPatientsSeen      { get; set; }
    public          int        NewPatients            { get; set; }
    public          int        ReturnPatients         { get; set; }
    public          decimal    AverageConsultationMinutes { get; set; }

    // ——— ৪. রেভিনিউ ও শেয়ারিং (Decimal Fields) ———
    public          decimal    TotalRevenue           { get; set; }
    public          decimal    ConsultationRevenue    { get; set; }
    public          decimal    TelemedicineRevenue    { get; set; }
    public          decimal    PlatformShareAmount    { get; set; }
    public          decimal    DoctorShareAmount      { get; set; }

    // ——— ৫. রেটিং ও রিপোর্ট স্ট্যাটাস ———
    public          decimal    AverageRating          { get; set; }
    public          int        TotalReviews           { get; set; }
    public          int        FiveStarReviews        { get; set; }
    public          int        OneStarReviews         { get; set; }
    public          string?    GeneratedBy            { get; set; }
    public          DateTime   GeneratedAt            { get; set; } = DateTime.UtcNow;
    public          string?    PdfUrl                 { get; set; }
    public          string?    Department             { get; set; }
    public          ReportStatus Status               { get; set; } = ReportStatus.Draft;
}