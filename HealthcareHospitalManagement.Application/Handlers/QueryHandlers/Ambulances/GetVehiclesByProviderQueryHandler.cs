using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Ambulance;
using HealthcareHospitalManagement.Application.Queries.Ambulances;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Ambulances
{
    public sealed class GetVehiclesByProviderQueryHandler(IAmbulanceUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetVehiclesByProviderQuery, ApiResponseDto<PagedResultDto<AmbulanceVehicleResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<AmbulanceVehicleResponseDto>>> Handle(
            GetVehiclesByProviderQuery request, CancellationToken ct)
        {
            var allData = (await uow.AmbulanceVehicles.GetByProviderIdAsync(request.ProviderId, ct)).ToList();
            var totalRecords = allData.Count;

            var pagedResponse = PagedResponse<AmbulanceVehicle>.Create(
                data: allData.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize),
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                totalRecords: totalRecords);

            var result = pagedResponse.ToMappedPagedResult<AmbulanceVehicle, AmbulanceVehicleResponseDto>(mapper);
            return ApiResponseDto<PagedResultDto<AmbulanceVehicleResponseDto>>.Ok(result);
        }
    }

}
