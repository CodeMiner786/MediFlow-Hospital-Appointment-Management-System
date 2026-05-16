using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using HealthcareHospitalManagement.Application.Queries.Pharmacys;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Pharmacys
{
    public sealed class GetMedicineOrderByIdQueryHandler(IPharmacyUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetMedicineOrderByIdQuery, ApiResponseDto<MedicineOrderResponseDto>>
    {
        public async Task<ApiResponseDto<MedicineOrderResponseDto>> Handle(
            GetMedicineOrderByIdQuery request, CancellationToken ct)
        {
            var order = await uow.MedicineOrders.GetByIdAsync(request.OrderId, ct);
            if (order is null)
                return ApiResponseDto<MedicineOrderResponseDto>.Fail(
                    $"OrderId '{request.OrderId}' পাওয়া যায়নি।");

            var result = mapper.Map<MedicineOrderResponseDto>(order);
            return ApiResponseDto<MedicineOrderResponseDto>.Ok(result);
        }
    }

}
