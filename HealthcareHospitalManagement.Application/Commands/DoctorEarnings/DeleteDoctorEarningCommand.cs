using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorEarnings
{
    public record DeleteDoctorEarningCommand(Guid EarningId)
    : IRequest<ApiResponseDto<bool>>;

}
