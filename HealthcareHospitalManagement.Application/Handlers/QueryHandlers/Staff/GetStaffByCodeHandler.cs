using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Staff;
using HealthcareHospitalManagement.Application.Queries.Staff;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Staff
{
    public class GetStaffByCodeHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetStaffByCodeQuery, ApiResponseDto<StaffDto>>
    {
        public async Task<ApiResponseDto<StaffDto>> Handle(
            GetStaffByCodeQuery request, CancellationToken ct)
        {
            var entity = await uow.Staff.GetByStaffCodeAsync(request.StaffCode);
            if (entity is null)
                return ApiResponseDto<StaffDto>.FailResponse("Staff member not found.");

            return ApiResponseDto<StaffDto>.SuccessResponse(mapper.Map<StaffDto>(entity));
        }
    }

}
