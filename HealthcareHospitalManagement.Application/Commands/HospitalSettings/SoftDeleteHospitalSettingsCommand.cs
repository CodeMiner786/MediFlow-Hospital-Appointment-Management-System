using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.HospitalSettings
{
    public record SoftDeleteHospitalSettingsCommand(Guid SettingsId)
    : IRequest<ApiResponseDto<bool>>;

}
