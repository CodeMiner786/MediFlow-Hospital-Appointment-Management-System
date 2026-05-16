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
    public sealed class GetBedsByFilterQueryHandler(IWardEmergencyUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetBedsByFilterQuery, ApiResponseDto<PagedResultDto<BedResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<BedResponseDto>>> Handle(
            GetBedsByFilterQuery request, CancellationToken ct)
        {
            var dto = request.Dto;

            // ── WardId থাকলে Ward ভিত্তিক, না থাকলে সব Bed ──
            IEnumerable<Bed> rawData;

            if (dto.WardId.HasValue && dto.Status.HasValue)
            {
                // Ward + Status উভয় ফিল্টার
                var wardBeds = await uow.Beds.GetBedsByWardIdAsync(dto.WardId.Value, ct);
                rawData = wardBeds.Where(b => b.Status == dto.Status.Value);
            }
            else if (dto.WardId.HasValue)
            {
                rawData = await uow.Beds.GetBedsByWardIdAsync(dto.WardId.Value, ct);
            }
            else
            {
                rawData = await uow.Beds.GetAllAsync(ct);
                if (dto.Status.HasValue)
                    rawData = rawData.Where(b => b.Status == dto.Status.Value);
            }

            var dataList = rawData.ToList();
            var totalRecords = dataList.Count;

            var pagedResponse = PagedResponse<Bed>.Create(
                data: dataList.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize),
                pageNumber: dto.PageNumber,
                pageSize: dto.PageSize,
                totalRecords: totalRecords);

            var result = pagedResponse.ToMappedPagedResult<Bed, BedResponseDto>(mapper);
            return ApiResponseDto<PagedResultDto<BedResponseDto>>.Ok(result);
        }
    }

}
