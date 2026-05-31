using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorEarning;
using HealthcareHospitalManagement.Application.Helpers.Stream;
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
    public class GetEarningsByDoctorHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetEarningsByDoctorQuery, ApiResponseDto<PagedResultDto<DoctorEarningDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorEarningDto>>> Handle(
            GetEarningsByDoctorQuery request, CancellationToken ct)
        {
            var stream = uow.DoctorEarnings.GetEarningsByDoctorIdStream(request.DoctorId);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorEarning, DoctorEarningDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorEarningDto>>.SuccessResponse(paged);
        }
    }

}
