using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Doctors;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Doctors;

public class CreateDoctorHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateDoctorCommand, Guid>
{
    public async Task<Guid> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        // ✅ request.Doctor → request.Dto
        var doctor = mapper.Map<DoctorEntity>(request.Dto);

        // ✅ DoctorCode auto-generate
        doctor.DoctorCode = $"DOC-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}";

        await uow.Doctors.AddAsync(doctor, cancellationToken);
        await uow.SaveChangesAsync(cancellationToken);

        return doctor.Id;
    }
}