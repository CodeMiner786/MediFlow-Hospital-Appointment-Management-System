using HealthcareHospitalManagement.Application.DTOs.Department;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Department
{
    public record GetDepartmentWithDoctorsQuery(Guid DepartmentId)
    : IRequest<ApiResponseDto<DepartmentDto>>;

}
