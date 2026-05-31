using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Doctors;
using HealthcareHospitalManagement.Application.DTOs.Doctors;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Doctors
{
    public class CreateDoctorHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateDoctorCommand, ApiResponseDto<DoctorDto>>
    {
        public async Task<ApiResponseDto<DoctorDto>> Handle(
            CreateDoctorCommand request, CancellationToken ct)
        {
            var exists = await uow.Doctors.GetByDoctorCodeAsync(request.Dto.DoctorCode);
            if (exists is not null)
                return ApiResponseDto<DoctorDto>.FailResponse("Doctor with this code already exists.");

            var entity = mapper.Map<Domain.Entities.Doctor.DoctorEntity>(request.Dto);

            await uow.Doctors.AddAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorDto>.SuccessResponse(
                mapper.Map<DoctorDto>(entity), "Doctor created successfully.");
        }
    }

}
