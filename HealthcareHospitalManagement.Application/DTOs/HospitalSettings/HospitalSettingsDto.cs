using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.HospitalSettings
{
    public class HospitalSettingsDto
    {
        public Guid Id { get; set; }
        public decimal DefaultPlatformSharePercent { get; set; }
        public decimal DefaultDoctorSharePercent { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
