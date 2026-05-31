using HealthcareHospitalManagement.Application.DTOs.Department;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Department
{
    public record UpdateDepartmentCommand(Guid Id, UpdateDepartmentDto Dto)
    : IRequest<ApiResponseDto<DepartmentDto>>;

}
