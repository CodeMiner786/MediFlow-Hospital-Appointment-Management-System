using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Ambulance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Ambulances
{
    public sealed record GetAmbulanceProviderByIdQuery(Guid ProviderId)
    : IRequest<ApiResponseDto<AmbulanceProviderResponseDto>>;

}
