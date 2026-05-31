using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.HospitalSettings
{
    public class UpdateHospitalSettingsDto
    {
        public decimal DefaultPlatformSharePercent { get; set; }
        public decimal DefaultDoctorSharePercent { get; set; }
    }
}
