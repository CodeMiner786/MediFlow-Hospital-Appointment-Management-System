using HealthcareHospitalManagement.Application.DTOs.DoctorLeave;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorLeaves
{
    public record GetLeaveByIdQuery(Guid LeaveId)
    : IRequest<ApiResponseDto<DoctorLeaveDto>>;

}
