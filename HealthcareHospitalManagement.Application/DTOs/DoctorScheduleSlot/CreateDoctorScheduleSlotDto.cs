using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorScheduleSlot
{
    public class CreateDoctorScheduleSlotDto
    {
        public Guid DoctorScheduleId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime SlotDate { get; set; }
        public TimeOnly SlotStartTime { get; set; }
        public TimeOnly SlotEndTime { get; set; }
        public bool IsTelemedicine { get; set; } = false;
    }

}
