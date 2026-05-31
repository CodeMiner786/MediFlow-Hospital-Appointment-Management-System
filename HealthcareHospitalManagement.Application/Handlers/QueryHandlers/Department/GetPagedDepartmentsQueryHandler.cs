using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Department;
using HealthcareHospitalManagement.Application.Queries.Department;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HealthcareHospitalManagement.Domain.Entities.Doctor;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Department
{
    public sealed class GetPagedDepartmentsQueryHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetPagedDepartmentsQuery, ApiResponseDto<PagedResultDto<DepartmentDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DepartmentDto>>> Handle(
            GetPagedDepartmentsQuery request, CancellationToken cancellationToken)
        {
            // Step 1: Repository থেকে Domain PagedResponse<Department> আনা
            var pagedResponse = await uow.Departments
                .GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);

            // Step 2: এখানে Alias (DomainDepartment) ব্যবহার করা হয়েছে
            var pagedResult = pagedResponse.ToMappedPagedResult<DepartmentEntity, DepartmentDto>(mapper);

            // Step 3: ApiResponseDto তে wrap করে return
            return ApiResponseDto<PagedResultDto<DepartmentDto>>.SuccessResponse(pagedResult);
        }
    }
}