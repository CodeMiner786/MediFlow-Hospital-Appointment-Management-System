using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Staff;
using HealthcareHospitalManagement.Application.Helpers.Stream;
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
    public class GetAllStaffHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetAllStaffQuery, ApiResponseDto<PagedResultDto<StaffDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<StaffDto>>> Handle(
            GetAllStaffQuery request, CancellationToken ct)
        {
            var all = await uow.Staff.GetAllAsync(ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.StaffEntity, StaffDto>(
                            [.. all], request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<StaffDto>>.SuccessResponse(paged);
        }
    }

}
