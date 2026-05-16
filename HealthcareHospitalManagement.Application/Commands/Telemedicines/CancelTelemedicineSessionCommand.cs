using HealthcareHospitalManagement.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Telemedicines
{
    public sealed record CancelTelemedicineSessionCommand(Guid SessionId)
    : IRequest<ApiResponseDto<bool>>;

}
