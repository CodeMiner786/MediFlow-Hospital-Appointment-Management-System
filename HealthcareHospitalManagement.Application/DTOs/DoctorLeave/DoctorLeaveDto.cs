using HealthcareHospitalManagement.Domain.Enums.DoctorLeave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorLeave
{
    public class DoctorLeaveDto
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; } = string.Empty;
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DoctorLeaveType LeaveType { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? ApprovedBy { get; set; }
        public string? RejectionReason { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
