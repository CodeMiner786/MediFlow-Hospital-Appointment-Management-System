using HealthcareHospitalManagement.Domain.Enums.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorPerformanceReport
{
    public class CreateDoctorPerformanceReportDto
    {
        public Guid DoctorId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public ReportPeriodType PeriodType { get; set; }
        public string? GeneratedBy { get; set; }
        public string? Department { get; set; }
    }
}
