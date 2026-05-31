using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Staff
{
    public record DeleteStaffCommand(Guid StaffId)
    : IRequest<ApiResponseDto<bool>>;

}
