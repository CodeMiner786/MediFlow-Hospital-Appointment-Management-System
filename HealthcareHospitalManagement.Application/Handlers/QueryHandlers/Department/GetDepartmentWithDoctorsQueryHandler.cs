using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Department;
using HealthcareHospitalManagement.Application.Queries.Department;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Department
{
    public sealed class GetDepartmentWithDoctorsQueryHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetDepartmentWithDoctorsQuery, ApiResponseDto<DepartmentDto>>
    {
        public async Task<ApiResponseDto<DepartmentDto>> Handle(
            GetDepartmentWithDoctorsQuery request, CancellationToken cancellationToken)
        {
            var entity = await uow.Departments
                .GetDepartmentWithDoctorsAsync(request.DepartmentId);

            if (entity is null)
                return ApiResponseDto<DepartmentDto>.FailResponse("Department not found.");

            return ApiResponseDto<DepartmentDto>.SuccessResponse(mapper.Map<DepartmentDto>(entity));
        }
    }

}
