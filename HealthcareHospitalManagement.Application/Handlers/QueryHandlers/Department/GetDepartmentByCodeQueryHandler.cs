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
    public sealed class GetDepartmentByCodeQueryHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetDepartmentByCodeQuery, ApiResponseDto<DepartmentDto>>
    {
        public async Task<ApiResponseDto<DepartmentDto>> Handle(
            GetDepartmentByCodeQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Code))
                return ApiResponseDto<DepartmentDto>.FailResponse("Department code cannot be empty.");

            var entity = await uow.Departments.GetByCodeAsync(request.Code);

            if (entity is null)
                return ApiResponseDto<DepartmentDto>.FailResponse(
                    $"No department found with code '{request.Code}'.");

            return ApiResponseDto<DepartmentDto>.SuccessResponse(mapper.Map<DepartmentDto>(entity));
        }
    }


}
