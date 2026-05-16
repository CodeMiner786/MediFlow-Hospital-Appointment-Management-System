using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using HealthcareHospitalManagement.Application.Queries.Pharmacys;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Pharmacys
{
    public sealed class GetMedicinesByPharmacyQueryHandler(IPharmacyUnitOfWork uow, IMapper mapper)
        : IRequestHandler<GetMedicinesByPharmacyQuery, ApiResponseDto<PagedResultDto<MedicineResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<MedicineResponseDto>>> Handle(
            GetMedicinesByPharmacyQuery request, CancellationToken ct)
        {
            var allData = (await uow.Medicines.GetMedicinesByPharmacyIdAsync(request.PharmacyId, ct)).ToList();
            var totalRecords = allData.Count;

            var pagedData = allData
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList(); // 🔹 ToList() যোগ করা হলো

            var pagedResponse = PagedResponse<Domain.Entities.Pharmacy.Medicine>.Create(
                data: pagedData,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                totalRecords: totalRecords);

            var result = pagedResponse.ToMappedPagedResult<
                Domain.Entities.Pharmacy.Medicine,
                MedicineResponseDto>(mapper);

            return ApiResponseDto<PagedResultDto<MedicineResponseDto>>.Ok(result);
        }
    }
}
