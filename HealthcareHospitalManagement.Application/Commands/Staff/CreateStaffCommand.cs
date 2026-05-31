using HealthcareHospitalManagement.Application.DTOs.Staff;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Staff
{
    public record CreateStaffCommand(CreateStaffDto Dto)
    : IRequest<ApiResponseDto<StaffDto>>;

}
