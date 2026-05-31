using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorScheduleSlot
{
    public class DoctorScheduleSlotDto
    {
        public Guid Id { get; set; }
        public Guid DoctorScheduleId { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; } = string.Empty;
        public DateTime SlotDate { get; set; }
        public TimeOnly SlotStartTime { get; set; }
        public TimeOnly SlotEndTime { get; set; }
        public bool IsBooked { get; set; }
        public bool IsBlocked { get; set; }
        public string? BlockReason { get; set; }
        public bool IsTelemedicine { get; set; }
        public Guid? AppointmentId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
