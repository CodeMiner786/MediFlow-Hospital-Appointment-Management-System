using HealthcareHospitalManagement.Application.DTOs.DoctorLeave;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorLeaves
{
    public record ApproveDoctorLeaveCommand(Guid LeaveId, ApproveDoctorLeaveDto Dto)
    : IRequest<ApiResponseDto<bool>>;

}
