using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorAvailabilityLog;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorAvailablityLog
{
    public sealed record GetDoctorAvailabilityLogsByStatusQuery(
    DoctorAvailabilityStatus Status,
    int PageNumber = 1,
    int PageSize = 10) : IRequest<ApiResponseDto<PagedResultDto<DoctorAvailabilityLogDto>>>;

}
