using HealthcareHospitalManagement.Domain.Enums.DoctorLeave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorLeave
{
    public class UpdateDoctorLeaveDto
    {
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DoctorLeaveType LeaveType { get; set; }
        public string? Notes { get; set; }
    }


}
