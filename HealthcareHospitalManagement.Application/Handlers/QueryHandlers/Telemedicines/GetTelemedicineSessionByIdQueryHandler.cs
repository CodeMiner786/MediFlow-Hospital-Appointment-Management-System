using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Telemedicine;
using HealthcareHospitalManagement.Application.Queries.Telemedicines;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Telemedicines
{
    public sealed class GetTelemedicineSessionByIdQueryHandler(ITelemedicineUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetTelemedicineSessionByIdQuery, ApiResponseDto<TelemedicineSessionResponseDto>>
    {
        public async Task<ApiResponseDto<TelemedicineSessionResponseDto>> Handle(
            GetTelemedicineSessionByIdQuery request, CancellationToken ct)
        {
            var session = await uow.TelemedicineSessions.GetByIdAsync(request.SessionId, ct);
            if (session is null)
                return ApiResponseDto<TelemedicineSessionResponseDto>.Fail(
                    $"SessionId '{request.SessionId}' পাওয়া যায়নি।");

            var result = mapper.Map<TelemedicineSessionResponseDto>(session);
            return ApiResponseDto<TelemedicineSessionResponseDto>.Ok(result);
        }
    }

}
