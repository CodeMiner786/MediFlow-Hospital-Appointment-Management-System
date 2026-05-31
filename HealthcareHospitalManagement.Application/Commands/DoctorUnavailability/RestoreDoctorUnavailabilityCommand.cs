using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorUnavailability
{
    public record RestoreDoctorUnavailabilityCommand(Guid UnavailabilityId)
    : IRequest<ApiResponseDto<bool>>;

}
