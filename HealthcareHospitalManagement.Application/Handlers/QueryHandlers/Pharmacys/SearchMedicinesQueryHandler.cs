using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using HealthcareHospitalManagement.Application.Queries.Pharmacys;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Pharmacys
{
    public sealed class SearchMedicinesQueryHandler(IPharmacyUnitOfWork uow, IMapper mapper)
    : IRequestHandler<SearchMedicinesQuery, ApiResponseDto<PagedResultDto<MedicineResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<MedicineResponseDto>>> Handle(
            SearchMedicinesQuery request, CancellationToken ct)
        {
            var dto = request.Dto;

            // ── ① Category বা SearchTerm দিয়ে DB থেকে ডেটা আনা ──
            var rawData = dto.Category.HasValue
                ? await uow.Medicines.GetByCategoryAsync(dto.Category.Value, ct)
                : await uow.Medicines.SearchMedicinesAsync(
                    dto.Name ?? dto.GenericName ?? string.Empty, ct);

            // ── ② RequiresPrescription ফিল্টার (in-memory) ──
            if (dto.RequiresPrescription.HasValue)
                rawData = rawData.Where(m => m.RequiresPrescription == dto.RequiresPrescription.Value);

            var dataList = rawData.ToList();
            var totalRecords = dataList.Count;

            // ── ③ PagedResponse তৈরি করা ──
            // PagedResponse.Create() → pagination metadata সহ wrapper
            var pagedResponse = PagedResponse<Domain.Entities.Pharmacy.Medicine>.Create(
                data: dataList.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize),
                pageNumber: dto.PageNumber,
                pageSize: dto.PageSize,
                totalRecords: totalRecords);

            // ── ④ PagingExtensions দিয়ে AutoMapper mapping + PagedResultDto তৈরি ──
            // ToMappedPagedResult<TSource, TDestination>() একসাথে map ও wrap করে
            var result = pagedResponse.ToMappedPagedResult<
                Domain.Entities.Pharmacy.Medicine,
                MedicineResponseDto>(mapper);

            return ApiResponseDto<PagedResultDto<MedicineResponseDto>>.Ok(result);
        }
    }

}
