using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorUnavailability;
using HealthcareHospitalManagement.Application.Helpers.Stream;
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
    public class GetUnavailabilitiesByDoctorHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetUnavailabilitiesByDoctorQuery, ApiResponseDto<PagedResultDto<DoctorUnavailabilityDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorUnavailabilityDto>>> Handle(
            GetUnavailabilitiesByDoctorQuery request, CancellationToken ct)
        {
            var all = await uow.DoctorUnavailabilities.FindAsync(
                            x => x.DoctorId == request.DoctorId, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorUnavailability, DoctorUnavailabilityDto>(
                            [.. all], request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorUnavailabilityDto>>.SuccessResponse(paged);
        }
    }

}
