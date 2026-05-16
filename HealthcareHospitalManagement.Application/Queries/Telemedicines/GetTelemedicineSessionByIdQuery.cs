using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Telemedicine;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Telemedicines
{
    public sealed record GetTelemedicineSessionByIdQuery(Guid SessionId)
    : IRequest<ApiResponseDto<TelemedicineSessionResponseDto>>;

}
