using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Staff.StaffAttendance
{
    public class StaffAttendanceDto
    {
        public Guid Id { get; set; }
        public Guid StaffId { get; set; }
        public string StaffFullName { get; set; } = string.Empty;
        public DateOnly AttendanceDate { get; set; }
        public TimeOnly? CheckInTime { get; set; }
        public TimeOnly? CheckOutTime { get; set; }
        public bool IsPresent { get; set; }
        public bool IsOnLeave { get; set; }
        public string? LeaveReason { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
