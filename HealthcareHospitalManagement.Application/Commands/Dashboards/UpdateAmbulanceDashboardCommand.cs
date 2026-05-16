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
    public record UpdateAmbulanceDashboardCommand(
    Guid Id,
    int? TotalVehicles,
    int? AvailableVehicles,
    int? ActiveBookings,
    int? TodayBookings,
    int? CompletedToday,
    decimal? TodayRevenue,
    decimal? MonthlyRevenue,
    decimal? WalletBalance,
    decimal? AverageRating
    ) : IRequest<ApiResponseDto<AmbulanceDashboardResponseDto>>;

}
