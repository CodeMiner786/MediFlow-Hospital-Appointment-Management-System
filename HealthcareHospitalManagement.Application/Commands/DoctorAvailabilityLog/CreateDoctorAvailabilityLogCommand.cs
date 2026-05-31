using HealthcareHospitalManagement.Application.DTOs.DoctorAvailabilityLog;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorAvailabilityLog
{
    public sealed record CreateDoctorAvailabilityLogCommand(
    CreateDoctorAvailabilityLogDto Dto) : IRequest<ApiResponseDto<DoctorAvailabilityLogDto>>;

}
