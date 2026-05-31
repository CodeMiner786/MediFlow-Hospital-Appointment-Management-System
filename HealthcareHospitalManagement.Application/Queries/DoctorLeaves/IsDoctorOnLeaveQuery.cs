using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorLeaves
{
    public record IsDoctorOnLeaveQuery(Guid DoctorId, DateTime Date)
    : IRequest<ApiResponseDto<bool>>;

}
