using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Doctors;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Doctors
{
    public class CreateDoctorLeaveHandler(IDoctorUnitOfWork uow, IMapper mapper)
        : IRequestHandler<CreateDoctorLeaveCommand, Guid>
    {
        public async Task<Guid> Handle(CreateDoctorLeaveCommand request, CancellationToken cancellationToken)
        {
            // DTO → Entity mapping
            var leaveEntity = mapper.Map<DoctorLeave>(request.Dto);

            await uow.DoctorLeaves.AddAsync(leaveEntity, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return leaveEntity.Id;
        }
    }
}
