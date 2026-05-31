using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorLeave
{
    public class ApproveDoctorLeaveDto
    {
        public bool IsApproved { get; set; }
        public string ApprovedBy { get; set; } = string.Empty;
        public string? RejectionReason { get; set; }
    }
}
