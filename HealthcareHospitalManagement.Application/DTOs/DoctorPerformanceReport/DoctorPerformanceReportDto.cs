using HealthcareHospitalManagement.Domain.Enums.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorPerformanceReport
{
    public class DoctorPerformanceReportDto
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; } = string.Empty;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public ReportPeriodType PeriodType { get; set; }
        public int TotalAppointments { get; set; }
        public int CompletedAppointments { get; set; }
        public int CancelledAppointments { get; set; }
        public int NoShows { get; set; }
        public decimal CompletionRate { get; set; }
        public int TotalPatientsSeen { get; set; }
        public int NewPatients { get; set; }
        public int ReturnPatients { get; set; }
        public decimal AverageConsultationMinutes { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal ConsultationRevenue { get; set; }
        public decimal TelemedicineRevenue { get; set; }
        public decimal PlatformShareAmount { get; set; }
        public decimal DoctorShareAmount { get; set; }
        public decimal AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public int FiveStarReviews { get; set; }
        public int OneStarReviews { get; set; }
        public string? GeneratedBy { get; set; }
        public DateTime GeneratedAt { get; set; }
        public string? PdfUrl { get; set; }
        public string? Department { get; set; }
        public ReportStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
