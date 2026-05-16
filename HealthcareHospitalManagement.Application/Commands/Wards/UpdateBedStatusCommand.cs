using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Domain.Enums.WardBed;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Wards
{
    public sealed record UpdateBedStatusCommand(Guid BedId, BedStatus NewStatus)
    : IRequest<ApiResponseDto<bool>>;

}
