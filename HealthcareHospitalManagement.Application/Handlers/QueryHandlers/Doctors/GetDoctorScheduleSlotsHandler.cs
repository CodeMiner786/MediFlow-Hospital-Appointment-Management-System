using HealthcareHospitalManagement.Application.DTOs.Doctor;
using HealthcareHospitalManagement.Application.Queries.Doctors;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Doctors
{
    public class GetDoctorScheduleSlotsHandler(IDoctorUnitOfWork uow)
        : IRequestHandler<GetDoctorScheduleSlotsQuery, IEnumerable<DoctorScheduleSlotResponseDto>>
    {
        public async Task<IEnumerable<DoctorScheduleSlotResponseDto>> Handle(GetDoctorScheduleSlotsQuery request, CancellationToken cancellationToken)
        {
            var schedule = await uow.DoctorSchedules.GetQueryable()
                .FirstOrDefaultAsync(s => s.DoctorId == request.DoctorId && s.DayOfWeek == request.DayOfWeek, cancellationToken);

            if (schedule == null)
                return Enumerable.Empty<DoctorScheduleSlotResponseDto>();

            var slots = new List<DoctorScheduleSlotResponseDto>();
            var current = schedule.StartTime;
            while (current < schedule.EndTime)
            {
                var end = current.AddMinutes(schedule.SlotDurationMinutes);

                slots.Add(new DoctorScheduleSlotResponseDto
                {
                    SlotId = Guid.NewGuid(), // slot unique id
                    StartTime = current,
                    EndTime = end,
                    IsBooked = false, // এখানে future booking check করতে হবে
                    IsAvailable = true
                });

                current = end;
            }

            return slots;
        }
    }
}
