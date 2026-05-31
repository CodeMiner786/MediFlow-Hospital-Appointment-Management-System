using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorSchedule
{
    public class UpdateDoctorScheduleDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public ShiftType ShiftType { get; set; }
        public int MaxAppointments { get; set; }
        public int SlotDurationMinutes { get; set; }
        public bool IsAvailable { get; set; }
        public string? Location { get; set; }
        public bool IsTelemedicineSlot { get; set; }
        public string SetBy { get; set; } = string.Empty;
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }

}
