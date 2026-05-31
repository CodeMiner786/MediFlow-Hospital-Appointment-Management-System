using HealthcareHospitalManagement.Application.DTOs.Staff;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Staff
{
    public record GetStaffByCodeQuery(string StaffCode)
    : IRequest<ApiResponseDto<StaffDto>>;

}
