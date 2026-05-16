using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Emergency;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Emergencys
{
    public sealed record DischargeEmergencyPatientCommand(Guid EmergencyVisitId)
    : IRequest<ApiResponseDto<EmergencyVisitResponseDto>>;

}
