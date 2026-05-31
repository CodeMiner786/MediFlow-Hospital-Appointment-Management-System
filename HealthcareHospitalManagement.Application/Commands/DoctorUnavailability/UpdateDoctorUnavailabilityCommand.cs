using HealthcareHospitalManagement.Application.DTOs.DoctorUnavailability;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorUnavailability
{
    public record UpdateDoctorUnavailabilityCommand(Guid UnavailabilityId, UpdateDoctorUnavailabilityDto Dto)
    : IRequest<ApiResponseDto<DoctorUnavailabilityDto>>;

}
