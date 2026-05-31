using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorLeaves
{
    public record RestoreDoctorLeaveCommand(Guid LeaveId)
    : IRequest<ApiResponseDto<bool>>;


}
