using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.HospitalSettings
{
    public class CreateHospitalSettingsDto
    {
        public decimal DefaultPlatformSharePercent { get; set; } = 20;
        public decimal DefaultDoctorSharePercent { get; set; } = 80;
    }

}
