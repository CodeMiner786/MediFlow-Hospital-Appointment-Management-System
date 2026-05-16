using HealthcareHospitalManagement.Application.DTOs.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Doctors
{
    public record GetDoctorScheduleSlotsQuery(Guid DoctorId, DayOfWeek DayOfWeek)
        : IRequest<IEnumerable<DoctorScheduleSlotResponseDto>>;
}
