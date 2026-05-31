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
    public sealed class GetDepartmentByIdQueryHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetDepartmentByIdQuery, ApiResponseDto<DepartmentDto>>
    {
        public async Task<ApiResponseDto<DepartmentDto>> Handle(
            GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await uow.Departments.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null)
                return ApiResponseDto<DepartmentDto>.FailResponse("Department not found.");

            return ApiResponseDto<DepartmentDto>.SuccessResponse(mapper.Map<DepartmentDto>(entity));
        }
    }

}
