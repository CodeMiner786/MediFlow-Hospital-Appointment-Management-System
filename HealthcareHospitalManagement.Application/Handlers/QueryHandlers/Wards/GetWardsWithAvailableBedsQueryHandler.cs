using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Wards;
using HealthcareHospitalManagement.Application.Queries.Words;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Wards;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Wards
{
    public sealed class GetWardsWithAvailableBedsQueryHandler(IWardEmergencyUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetWardsWithAvailableBedsQuery, ApiResponseDto<PagedResultDto<WardResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<WardResponseDto>>> Handle(
            GetWardsWithAvailableBedsQuery request, CancellationToken ct)
        {
            var allData = (await uow.Wards.GetWardsWithAvailableBedsAsync(ct)).ToList();
            var totalRecords = allData.Count;

            var pagedResponse = PagedResponse<Ward>.Create(
                data: allData.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize),
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                totalRecords: totalRecords);

            var result = pagedResponse.ToMappedPagedResult<Ward, WardResponseDto>(mapper);
            return ApiResponseDto<PagedResultDto<WardResponseDto>>.Ok(result);
        }
    }

}
