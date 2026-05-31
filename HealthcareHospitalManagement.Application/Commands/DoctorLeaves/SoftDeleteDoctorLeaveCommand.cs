using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorLeaves
{
    public record SoftDeleteDoctorLeaveCommand(Guid LeaveId)
    : IRequest<ApiResponseDto<bool>>;

}
