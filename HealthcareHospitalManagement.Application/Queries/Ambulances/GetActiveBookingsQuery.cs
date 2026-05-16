using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Ambulance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Ambulances
{
    public sealed record GetActiveBookingsQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<ApiResponseDto<PagedResultDto<AmbulanceBookingResponseDto>>>;

}
