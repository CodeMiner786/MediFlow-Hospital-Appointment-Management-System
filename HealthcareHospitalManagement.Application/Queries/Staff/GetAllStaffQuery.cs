using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Staff;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Staff
{
    public record GetAllStaffQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<ApiResponseDto<PagedResultDto<StaffDto>>>;

}
