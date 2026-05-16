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
    public sealed class GetMedicineByIdQueryHandler(IPharmacyUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetMedicineByIdQuery, ApiResponseDto<MedicineResponseDto>>
    {
        public async Task<ApiResponseDto<MedicineResponseDto>> Handle(
            GetMedicineByIdQuery request, CancellationToken ct)
        {
            var medicine = await uow.Medicines.GetByIdAsync(request.MedicineId, ct);
            if (medicine is null)
                return ApiResponseDto<MedicineResponseDto>.Fail(
                    $"MedicineId '{request.MedicineId}' পাওয়া যায়নি।");

            var result = mapper.Map<MedicineResponseDto>(medicine);
            return ApiResponseDto<MedicineResponseDto>.Ok(result);
        }
    }

}
