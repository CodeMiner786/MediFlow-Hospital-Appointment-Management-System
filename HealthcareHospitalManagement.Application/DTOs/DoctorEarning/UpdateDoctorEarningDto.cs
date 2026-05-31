using HealthcareHospitalManagement.Domain.Enums.DoctorEarning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorEarning
{
    public class UpdateDoctorEarningDto
    {
        public DateTime EarningDate { get; set; }
        public decimal TotalFee { get; set; }
        public decimal HospitalSharePercent { get; set; }
        public DoctorEarningType EarningType { get; set; }
        public string? Notes { get; set; }
    }

}
