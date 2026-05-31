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
    public sealed class GetDepartmentByNameQueryHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetDepartmentByNameQuery, ApiResponseDto<DepartmentDto>>
    {
        public async Task<ApiResponseDto<DepartmentDto>> Handle(
            GetDepartmentByNameQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return ApiResponseDto<DepartmentDto>.FailResponse("Department name cannot be empty.");

            var entity = await uow.Departments.GetByNameAsync(request.Name);

            if (entity is null)
                return ApiResponseDto<DepartmentDto>.FailResponse(
                    $"No department found with name '{request.Name}'.");

            return ApiResponseDto<DepartmentDto>.SuccessResponse(mapper.Map<DepartmentDto>(entity));
        }
    }

}
