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
    public sealed class SearchAmbulanceProvidersQueryHandler(IAmbulanceUnitOfWork uow, IMapper mapper)
    : IRequestHandler<SearchAmbulanceProvidersQuery, ApiResponseDto<PagedResultDto<AmbulanceProviderResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<AmbulanceProviderResponseDto>>> Handle(
            SearchAmbulanceProvidersQuery request, CancellationToken ct)
        {
            var dto = request.Dto;

            // ── IAsyncEnumerable stream দিয়ে City ভিত্তিক ভেরিফাইড প্রোভাইডার লোড ──
            // Repository তে GetVerifiedProvidersByCityStream() আছে
            var allData = new List<AmbulanceProviderProfile>();

            if (!string.IsNullOrWhiteSpace(dto.City))
            {
                await foreach (var provider in
                    uow.AmbulanceProviderProfiles.GetVerifiedProvidersByCityStream(dto.City, ct))
                {
                    allData.Add(provider);
                }
            }
            else
            {
                // City না দিলে GetQueryable() দিয়ে সব verified provider
                allData = uow.AmbulanceProviderProfiles
                    .GetQueryable()
                    .Where(p => p.IsVerified)
                    .ToList();
            }

            var totalRecords = allData.Count;
            var pagedResponse = PagedResponse<AmbulanceProviderProfile>.Create(
                data: allData.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize),
                pageNumber: dto.PageNumber,
                pageSize: dto.PageSize,
                totalRecords: totalRecords);

            var result = pagedResponse.ToMappedPagedResult<
                AmbulanceProviderProfile,
                AmbulanceProviderResponseDto>(mapper);

            return ApiResponseDto<PagedResultDto<AmbulanceProviderResponseDto>>.Ok(result);
        }
    }

}
