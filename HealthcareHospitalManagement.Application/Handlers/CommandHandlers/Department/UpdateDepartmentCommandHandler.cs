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
    public sealed class UpdateDepartmentCommandHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateDepartmentCommand, ApiResponseDto<DepartmentDto>>
    {
        public async Task<ApiResponseDto<DepartmentDto>> Handle(
            UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await uow.Departments.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null)
                return ApiResponseDto<DepartmentDto>.FailResponse("Department not found.");

            // ── Code uniqueness guard (নিজের Id বাদে) ──────────────────────────
            var codeOwner = await uow.Departments.GetByCodeAsync(request.Dto.Code);
            if (codeOwner is not null && codeOwner.Id != request.Id)
                return ApiResponseDto<DepartmentDto>.FailResponse(
                    $"Department code '{request.Dto.Code}' is already used by another department.");

            mapper.Map(request.Dto, entity);

            await uow.Departments.UpdateAsync(entity, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return ApiResponseDto<DepartmentDto>.SuccessResponse(
                mapper.Map<DepartmentDto>(entity), "Department updated successfully.");
        }
    }

}
