using HealthcareHospitalManagement.Application.DTOs.DoctorAvailabilityLog;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorAvailablityLog
{
    public sealed record GetDoctorAvailabilityLogByIdQuery(Guid LogId) 
        : IRequest<ApiResponseDto<DoctorAvailabilityLogDto>>;

}
