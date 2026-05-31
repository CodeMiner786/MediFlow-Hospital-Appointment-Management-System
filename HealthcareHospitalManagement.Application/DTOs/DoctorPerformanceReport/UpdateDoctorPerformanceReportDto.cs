using HealthcareHospitalManagement.Domain.Enums.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorPerformanceReport
{
    public class UpdateDoctorPerformanceReportDto
    {
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
        public string? PdfUrl { get; set; }
        public ReportStatus Status { get; set; }
    }

}
