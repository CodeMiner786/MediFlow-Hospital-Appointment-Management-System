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
    public record CreatePharmacyDashboardCommand(
    Guid PharmacyProfileId,
    DateOnly MetricDate,
    int PendingOrders,
    int ProcessingOrders,
    int CompletedToday,
    int TotalOrdersThisMonth,
    decimal TodayRevenue,
    decimal MonthlyRevenue,
    decimal WalletBalance,
    int LowStockMedicines,
    int OutOfStockMedicines,
    int ExpiringMedicines
    ) : IRequest<ApiResponseDto<PharmacyDashboardResponseDto>>;

}
