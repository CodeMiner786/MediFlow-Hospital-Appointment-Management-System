using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorLeave;
using HealthcareHospitalManagement.Application.Helpers.Stream;
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
    public class GetPendingLeavesHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetPendingLeavesQuery, ApiResponseDto<PagedResultDto<DoctorLeaveDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorLeaveDto>>> Handle(
            GetPendingLeavesQuery request, CancellationToken ct)
        {
            var stream = uow.DoctorLeaves.GetPendingLeavesStream();
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorLeave, DoctorLeaveDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorLeaveDto>>.SuccessResponse(paged);
        }
    }

}
