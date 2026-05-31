using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Department;
using HealthcareHospitalManagement.Application.Helpers.Stream;
using HealthcareHospitalManagement.Application.Queries.Department;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Department
{
    public sealed class GetDepartmentsByLocationQueryHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetDepartmentsByLocationQuery, ApiResponseDto<PagedResultDto<DepartmentDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DepartmentDto>>> Handle(
            GetDepartmentsByLocationQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Location))
                return ApiResponseDto<PagedResultDto<DepartmentDto>>.FailResponse(
                    "Location cannot be empty.");

            // Stream collect করে in-memory paginate
            var items = await StreamHelper.ToList(
                uow.Departments.GetDepartmentsByLocationStream(request.Location),
                cancellationToken);

            var pagedResult = StreamHelper.Paginate<DepartmentEntity, DepartmentDto>(
                items, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DepartmentDto>>.SuccessResponse(pagedResult);
        }
    }

}
