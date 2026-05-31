using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.DoctorUnavailability;
using HealthcareHospitalManagement.Application.Queries.DoctorUnavailability;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.DoctorUnavailability
{
    public class GetUnavailabilityByIdHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetUnavailabilityByIdQuery, ApiResponseDto<DoctorUnavailabilityDto>>
    {
        public async Task<ApiResponseDto<DoctorUnavailabilityDto>> Handle(
            GetUnavailabilityByIdQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorUnavailabilities.GetByIdAsync(request.UnavailabilityId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorUnavailabilityDto>.FailResponse("Unavailability record not found.");

            return ApiResponseDto<DoctorUnavailabilityDto>.SuccessResponse(
                mapper.Map<DoctorUnavailabilityDto>(entity));
        }
    }

}
