using HealthcareHospitalManagement.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Ambulances
{
    public sealed record CancelAmbulanceBookingCommand(Guid BookingId, string? CancellationReason)
    : IRequest<ApiResponseDto<bool>>;

}
