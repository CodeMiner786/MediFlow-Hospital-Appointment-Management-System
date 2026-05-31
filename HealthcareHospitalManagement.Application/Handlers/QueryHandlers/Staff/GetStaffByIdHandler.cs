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
    public class GetStaffByIdHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetStaffByIdQuery, ApiResponseDto<StaffDto>>
    {
        public async Task<ApiResponseDto<StaffDto>> Handle(
            GetStaffByIdQuery request, CancellationToken ct)
        {
            var entity = await uow.Staff.GetByIdAsync(request.StaffId, ct);
            if (entity is null)
                return ApiResponseDto<StaffDto>.FailResponse("Staff member not found.");

            return ApiResponseDto<StaffDto>.SuccessResponse(mapper.Map<StaffDto>(entity));
        }
    }
}
