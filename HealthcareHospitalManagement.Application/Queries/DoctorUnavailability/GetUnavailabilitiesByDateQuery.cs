using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorUnavailability;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorUnavailability
{
    public record GetUnavailabilitiesByDateQuery(Guid DoctorId, DateTime Date, int PageNumber = 1, int PageSize = 10)
    : IRequest<ApiResponseDto<PagedResultDto<DoctorUnavailabilityDto>>>;


}
