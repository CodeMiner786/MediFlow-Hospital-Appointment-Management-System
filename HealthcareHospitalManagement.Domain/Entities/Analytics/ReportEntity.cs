using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Analytics;

namespace HealthcareHospitalManagement.Domain.Entities.Analytics
{
    /// <summary>
    /// Archive for all generated administrative reports (Financial, Medical, or Operational).
    /// Stores metadata and links to physical files (PDF, Excel, Word).
    /// </summary>
    public class ReportEntity : BaseEntity
    {
        // ── Report Identity ────────────────────────────────────────────────────────────────────────────
        public string ReportTitle { get; set; } = string.Empty;
        public ReportType ReportType { get; set; }
        public ReportCategory Category { get; set; }

        // ── Date Range & Audit ─────────────────────────────────────────────────────────────────────────
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public string GeneratedBy { get; set; } = string.Empty;

        // ── Export Details (Links to Cloud Storage/Local Files) ────────────────────────────────────────
        public string? Summary { get; set; }   // JSON summary data
        public string? PdfUrl { get; set; }
        public string? ExcelUrl { get; set; }
        public string? WordUrl { get; set; }
    }
}