using HealthcareHospitalManagement.Application.DTOs.HospitalSettings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.HospitalSettings
{
    public record GetSettingsByIdQuery(Guid SettingsId)
    : IRequest<ApiResponseDto<HospitalSettingsDto>>;

}
