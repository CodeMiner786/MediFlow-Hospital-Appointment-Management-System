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
    public record CreateLabDashboardCommand(
    Guid LabProfileId,
    DateOnly MetricDate,
    int PendingOrders,
    int InProgressOrders,
    int CompletedToday,
    int TotalOrdersThisMonth,
    decimal TodayRevenue,
    decimal MonthlyRevenue,
    decimal WalletBalance,
    int UnpaidOrders
    ) : IRequest<ApiResponseDto<LabDashboardResponseDto>>;

}
