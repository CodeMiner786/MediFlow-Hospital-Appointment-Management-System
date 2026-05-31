using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Staff;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Staff
{
    public record GetStaffByShiftQuery(ShiftType ShiftType, int PageNumber = 1, int PageSize = 10)
    : IRequest<ApiResponseDto<PagedResultDto<StaffDto>>>;

}
