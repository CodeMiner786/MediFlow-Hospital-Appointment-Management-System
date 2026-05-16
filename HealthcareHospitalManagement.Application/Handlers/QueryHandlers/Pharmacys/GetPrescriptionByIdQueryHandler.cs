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
    public sealed class GetPrescriptionByIdQueryHandler(IPharmacyUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetPrescriptionByIdQuery, ApiResponseDto<PrescriptionResponseDto>>
    {
        public async Task<ApiResponseDto<PrescriptionResponseDto>> Handle(
            GetPrescriptionByIdQuery request, CancellationToken ct)
        {
            var prescription = await uow.Prescriptions.GetByIdAsync(request.PrescriptionId, ct);
            if (prescription is null)
                return ApiResponseDto<PrescriptionResponseDto>.Fail(
                    $"PrescriptionId '{request.PrescriptionId}' পাওয়া যায়নি।");

            var result = mapper.Map<PrescriptionResponseDto>(prescription);
            return ApiResponseDto<PrescriptionResponseDto>.Ok(result);
        }
    }

}
