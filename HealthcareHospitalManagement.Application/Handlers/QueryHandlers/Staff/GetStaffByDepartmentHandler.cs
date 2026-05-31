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
    public class GetStaffByDepartmentHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetStaffByDepartmentQuery, ApiResponseDto<PagedResultDto<StaffDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<StaffDto>>> Handle(
            GetStaffByDepartmentQuery request, CancellationToken ct)
        {
            var stream = uow.Staff.GetStaffByDepartmentStream(request.DepartmentId);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.StaffEntity, StaffDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<StaffDto>>.SuccessResponse(paged);
        }
    }

}
