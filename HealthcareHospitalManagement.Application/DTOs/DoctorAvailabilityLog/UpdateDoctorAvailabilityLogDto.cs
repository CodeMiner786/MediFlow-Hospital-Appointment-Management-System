using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorAvailabilityLog
{
    public class UpdateDoctorAvailabilityLogDto
    {
        public DoctorAvailabilityStatus Status { get; set; }
        public string? ChangedBy { get; set; }
        public string? Reason { get; set; }
        public DateTime? ExpectedBackAt { get; set; }
    }

}
