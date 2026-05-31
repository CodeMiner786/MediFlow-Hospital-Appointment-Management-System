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
    public class GetLeavesByDoctorIdHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetLeavesByDoctorIdQuery, ApiResponseDto<PagedResultDto<DoctorLeaveDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorLeaveDto>>> Handle(
            GetLeavesByDoctorIdQuery request, CancellationToken ct)
        {
            var stream = uow.DoctorLeaves.GetLeavesByDoctorIdStream(request.DoctorId);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorLeave, DoctorLeaveDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorLeaveDto>>.SuccessResponse(paged);
        }
    }

}
