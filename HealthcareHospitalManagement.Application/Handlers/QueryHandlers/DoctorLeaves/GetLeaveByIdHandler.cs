using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.DoctorLeave;
using HealthcareHospitalManagement.Application.Queries.DoctorLeaves;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.DoctorLeaves
{
    public class GetLeaveByIdHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetLeaveByIdQuery, ApiResponseDto<DoctorLeaveDto>>
    {
        public async Task<ApiResponseDto<DoctorLeaveDto>> Handle(
            GetLeaveByIdQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorLeaves.GetByIdAsync(request.LeaveId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorLeaveDto>.FailResponse("Leave record not found.");

            return ApiResponseDto<DoctorLeaveDto>.SuccessResponse(mapper.Map<DoctorLeaveDto>(entity));
        }
    }

}
