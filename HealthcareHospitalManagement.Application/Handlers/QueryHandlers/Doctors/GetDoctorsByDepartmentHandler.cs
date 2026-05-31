using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Doctors;
using HealthcareHospitalManagement.Application.Helpers.Stream;
using HealthcareHospitalManagement.Application.Queries.Doctors;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Doctors
{
    public class GetDoctorsByDepartmentHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetDoctorsByDepartmentQuery, ApiResponseDto<PagedResultDto<DoctorDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorDto>>> Handle(
            GetDoctorsByDepartmentQuery request, CancellationToken ct)
        {
            var stream = uow.Doctors.GetDoctorsByDepartmentStream(request.DepartmentId);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorEntity, DoctorDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorDto>>.SuccessResponse(paged);
        }
    }

}
