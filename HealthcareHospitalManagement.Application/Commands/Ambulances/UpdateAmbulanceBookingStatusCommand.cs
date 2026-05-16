using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Ambulance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Ambulances
{
    public sealed record UpdateAmbulanceBookingStatusCommand(UpdateAmbulanceBookingStatusRequestDto Dto)
    : IRequest<ApiResponseDto<AmbulanceBookingResponseDto>>;
}
