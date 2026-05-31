using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Department;
using HealthcareHospitalManagement.Application.DTOs.Department;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Department
{
    public sealed class CreateDepartmentCommandHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateDepartmentCommand, ApiResponseDto<DepartmentDto>>
    {
        public async Task<ApiResponseDto<DepartmentDto>> Handle(
            CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            // ── Duplicate code guard ──────────────────────────────────────────────
            var existing = await uow.Departments.GetByCodeAsync(request.Dto.Code);
            if (existing is not null)
                return ApiResponseDto<DepartmentDto>.FailResponse(
                    $"Department with code '{request.Dto.Code}' already exists.");

            var entity = mapper.Map<Domain.Entities.Doctor.DepartmentEntity>(request.Dto);

            await uow.Departments.AddAsync(entity, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return ApiResponseDto<DepartmentDto>.SuccessResponse(
                mapper.Map<DepartmentDto>(entity), "Department created successfully.");
        }
    }

}
