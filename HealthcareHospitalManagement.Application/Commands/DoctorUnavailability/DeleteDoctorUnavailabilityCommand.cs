using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorUnavailability
{
    public record DeleteDoctorUnavailabilityCommand(Guid UnavailabilityId)
    : IRequest<ApiResponseDto<bool>>;

}
