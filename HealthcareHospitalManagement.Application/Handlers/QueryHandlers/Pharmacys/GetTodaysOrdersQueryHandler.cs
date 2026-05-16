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
    public sealed class GetTodaysOrdersQueryHandler(IPharmacyUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetTodaysOrdersQuery, ApiResponseDto<PagedResultDto<MedicineOrderResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<MedicineOrderResponseDto>>> Handle(
            GetTodaysOrdersQuery request, CancellationToken ct)
        {
            var allData = (await uow.MedicineOrders.GetTodaysOrdersAsync(ct)).ToList();
            var totalRecords = allData.Count;

            var pagedResponse = PagedResponse<Domain.Entities.Pharmacy.MedicineOrder>.Create(
                data: allData.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize),
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                totalRecords: totalRecords);

            var result = pagedResponse.ToMappedPagedResult<
                Domain.Entities.Pharmacy.MedicineOrder,
                MedicineOrderResponseDto>(mapper);

            return ApiResponseDto<PagedResultDto<MedicineOrderResponseDto>>.Ok(result);
        }
    }
}
