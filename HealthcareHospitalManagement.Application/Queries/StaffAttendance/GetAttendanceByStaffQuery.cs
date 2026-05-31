using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Staff.StaffAttendance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.StaffAttendance
{
    public record GetAttendanceByStaffQuery(Guid StaffId, int PageNumber = 1, int PageSize = 10)
    : IRequest<ApiResponseDto<PagedResultDto<StaffAttendanceDto>>>;

}
