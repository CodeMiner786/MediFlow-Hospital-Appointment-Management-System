using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorScheduleSlot
{
    public class UpdateDoctorScheduleSlotDto
    {
        public DateTime SlotDate { get; set; }
        public TimeOnly SlotStartTime { get; set; }
        public TimeOnly SlotEndTime { get; set; }
        public bool IsBooked { get; set; }
        public bool IsBlocked { get; set; }
        public string? BlockReason { get; set; }
        public bool IsTelemedicine { get; set; }
        public Guid? AppointmentId { get; set; }
    }

}
