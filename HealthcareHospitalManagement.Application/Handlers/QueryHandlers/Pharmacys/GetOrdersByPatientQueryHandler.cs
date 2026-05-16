using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using HealthcareHospitalManagement.Application.Queries.Pharmacys;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Pharmacys
{
    public sealed class GetOrdersByPatientQueryHandler(IPharmacyUnitOfWork uow, IMapper mapper)
        : IRequestHandler<GetOrdersByPatientQuery, ApiResponseDto<PagedResultDto<MedicineOrderResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<MedicineOrderResponseDto>>> Handle(
            GetOrdersByPatientQuery request, CancellationToken ct)
        {
            var allData = (await uow.MedicineOrders.GetOrdersByPatientIdAsync(request.PatientId, ct)).ToList();
            var totalRecords = allData.Count;

            var pagedData = allData
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList(); // 🔹 এখানে ToList() যোগ করা হলো

            var pagedResponse = PagedResponse<Domain.Entities.Pharmacy.MedicineOrder>.Create(
                data: pagedData,
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
