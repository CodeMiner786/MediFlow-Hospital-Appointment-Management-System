using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorAvailabilityLog
{
    public class DoctorAvailabilityLogDto
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; } = string.Empty;
        public DoctorAvailabilityStatus Status { get; set; }
        public DateTime ChangedAt { get; set; }
        public string? ChangedBy { get; set; }
        public string? Reason { get; set; }
        public DateTime? ExpectedBackAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
