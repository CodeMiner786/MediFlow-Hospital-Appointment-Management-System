using HealthcareHospitalManagement.Application.DTOs.DoctorUnavailability;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorUnavailability
{
    public record GetUnavailabilityByIdQuery(Guid UnavailabilityId)
    : IRequest<ApiResponseDto<DoctorUnavailabilityDto>>;


}
