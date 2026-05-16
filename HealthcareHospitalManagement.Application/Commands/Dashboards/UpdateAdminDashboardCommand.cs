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
    public record UpdateAdminDashboardCommand(
    Guid Id,
    int? TotalDoctors,
    int? TotalPatients,
    int? TodayAppointments,
    int? TodayRevenue,
    decimal? MonthlyRevenue,
    int? PendingVerifications,
    int? ActiveAdmissions,
    int? AvailableBeds,
    int? ActiveAmbulances,
    int? PendingLabOrders,
    int? ActivePharmacyOrders,
    int? UnresolvedEmergencies
) : IRequest<ApiResponseDto<AdminDashboardResponseDto>>;

}
