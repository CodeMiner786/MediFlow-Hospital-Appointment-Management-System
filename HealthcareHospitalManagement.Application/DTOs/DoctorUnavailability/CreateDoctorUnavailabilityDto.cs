using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorUnavailability
{
    public class CreateDoctorUnavailabilityDto
    {
        public Guid DoctorId { get; set; }
        public DateTime UnavailableDate { get; set; }
        public TimeOnly? FromTime { get; set; }
        public TimeOnly? ToTime { get; set; }
        public string? Reason { get; set; }
        public bool IsFullDay { get; set; } = false;
    }

}
