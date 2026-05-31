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
    public class GetStaffByTypeHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetStaffByTypeQuery, ApiResponseDto<PagedResultDto<StaffDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<StaffDto>>> Handle(
            GetStaffByTypeQuery request, CancellationToken ct)
        {
            var stream = uow.Staff.GetStaffByTypeStream(request.StaffType);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.StaffEntity, StaffDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<StaffDto>>.SuccessResponse(paged);
        }
    }

}
