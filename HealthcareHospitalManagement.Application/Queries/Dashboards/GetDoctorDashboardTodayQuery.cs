using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Dashboard;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Dashboards
{
    public record GetDoctorDashboardTodayQuery(Guid DoctorId) 
        : IRequest<ApiResponseDto<DoctorDashboardResponseDto>>;

}
