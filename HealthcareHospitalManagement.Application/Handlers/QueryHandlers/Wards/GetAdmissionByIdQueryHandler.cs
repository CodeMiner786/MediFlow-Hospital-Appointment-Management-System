using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Wards;
using HealthcareHospitalManagement.Application.Queries.Words;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Wards
{
    public sealed class GetAdmissionByIdQueryHandler(IWardEmergencyUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetAdmissionByIdQuery, ApiResponseDto<AdmissionResponseDto>>
    {
        public async Task<ApiResponseDto<AdmissionResponseDto>> Handle(
            GetAdmissionByIdQuery request, CancellationToken ct)
        {
            var admission = await uow.Admissions.GetByIdAsync(request.AdmissionId, ct);
            if (admission is null)
                return ApiResponseDto<AdmissionResponseDto>.Fail(
                    $"AdmissionId '{request.AdmissionId}' পাওয়া যায়নি।");

            var result = mapper.Map<AdmissionResponseDto>(admission);
            return ApiResponseDto<AdmissionResponseDto>.Ok(result);
        }
    }

}
