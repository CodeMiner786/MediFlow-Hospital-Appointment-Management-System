using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Dashboards;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Dashboard;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Dashboards
{
    public sealed class UpdateAdminDashboardCommandHandler(
        IDashboardUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<UpdateAdminDashboardCommand, ApiResponseDto<AdminDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<AdminDashboardResponseDto>> Handle(
            UpdateAdminDashboardCommand request,
            CancellationToken cancellationToken)
        {
            var metric = await unitOfWork.AdminDashboardMetrics.GetByIdAsync(request.Id, cancellationToken);
            if (metric is null)
                return ApiResponseDto<AdminDashboardResponseDto>.Fail("ড্যাশবোর্ড মেট্রিক পাওয়া যায়নি।");

            if (request.TotalDoctors.HasValue) metric.ActiveDoctors = request.TotalDoctors.Value;
            if (request.PendingVerifications.HasValue) metric.PendingVerifications = request.PendingVerifications.Value;
            if (request.TodayAppointments.HasValue) metric.TotalAppointmentsToday = request.TodayAppointments.Value;
            if (request.TodayRevenue.HasValue) metric.PlatformRevenueToday = request.TodayRevenue.Value;
            if (request.MonthlyRevenue.HasValue) metric.MonthlyPlatformRevenue = request.MonthlyRevenue.Value;

            await unitOfWork.AdminDashboardMetrics.UpdateAsync(metric, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = mapper.Map<AdminDashboardResponseDto>(metric);
            return ApiResponseDto<AdminDashboardResponseDto>.Ok(dto, "Admin ড্যাশবোর্ড মেট্রিক সফলভাবে আপডেট হয়েছে।");
        }
    }
}
