using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Doctors;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Doctors
{
    public class CreateDoctorScheduleHandler(IDoctorUnitOfWork uow, IMapper mapper)
        : IRequestHandler<CreateDoctorScheduleCommand, Guid>
    {
        public async Task<Guid> Handle(CreateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            // DTO → Entity mapping
            var scheduleEntity = mapper.Map<DoctorSchedule>(request.Dto);

            await uow.DoctorSchedules.AddAsync(scheduleEntity, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return scheduleEntity.Id;
        }
    }
}
