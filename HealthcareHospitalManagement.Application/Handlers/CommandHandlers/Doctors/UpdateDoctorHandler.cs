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
    public class UpdateDoctorHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateDoctorCommand, ApiResponseDto<DoctorDto>>
    {
        public async Task<ApiResponseDto<DoctorDto>> Handle(
            UpdateDoctorCommand request, CancellationToken ct)
        {
            var entity = await uow.Doctors.GetByIdAsync(request.DoctorId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorDto>.FailResponse("Doctor not found.");

            mapper.Map(request.Dto, entity);

            await uow.Doctors.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorDto>.SuccessResponse(
                mapper.Map<DoctorDto>(entity), "Doctor updated successfully.");
        }
    }

}
