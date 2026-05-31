using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.DoctorEarning;
using HealthcareHospitalManagement.Application.Queries.DoctorEarnings;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.DoctorEarnings
{
    public class GetEarningByIdHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetEarningByIdQuery, ApiResponseDto<DoctorEarningDto>>
    {
        public async Task<ApiResponseDto<DoctorEarningDto>> Handle(
            GetEarningByIdQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorEarnings.GetByIdAsync(request.EarningId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorEarningDto>.FailResponse("Earning not found.");

            return ApiResponseDto<DoctorEarningDto>.SuccessResponse(
                mapper.Map<DoctorEarningDto>(entity));
        }
    }


}
