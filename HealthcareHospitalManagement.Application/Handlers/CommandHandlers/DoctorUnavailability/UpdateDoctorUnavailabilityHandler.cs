using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.DoctorUnavailability;
using HealthcareHospitalManagement.Application.DTOs.DoctorUnavailability;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorUnavailability
{
    public class UpdateDoctorUnavailabilityHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateDoctorUnavailabilityCommand, ApiResponseDto<DoctorUnavailabilityDto>>
    {
        public async Task<ApiResponseDto<DoctorUnavailabilityDto>> Handle(
            UpdateDoctorUnavailabilityCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorUnavailabilities.GetByIdAsync(request.UnavailabilityId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorUnavailabilityDto>.FailResponse("Unavailability record not found.");

            mapper.Map(request.Dto, entity);

            await uow.DoctorUnavailabilities.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorUnavailabilityDto>.SuccessResponse(
                mapper.Map<DoctorUnavailabilityDto>(entity), "Unavailability updated successfully.");
        }
    }

}
