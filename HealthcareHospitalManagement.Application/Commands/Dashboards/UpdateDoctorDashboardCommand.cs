using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Dashboard;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Dashboards
{
    public record UpdateDoctorDashboardCommand(
    Guid Id,
    int? TodayAppointments,
    int? PendingAppointments,
    int? CompletedToday,
    int? CancelledToday,
    int? TotalPatients,
    decimal? TodayEarnings,
    decimal? MonthlyEarnings,
    decimal? AverageRating,
    int? TotalReviews,
    bool? IsAvailable,
    int? ActiveTelemedicineSessions
    ) : IRequest<ApiResponseDto<DoctorDashboardResponseDto>>;

}
