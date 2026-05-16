using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Dashboards;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Dashboard;
using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Dashboards
{
    public sealed class CreateAdminDashboardCommandHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<CreateAdminDashboardCommand, ApiResponseDto<AdminDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<AdminDashboardResponseDto>> Handle(
            CreateAdminDashboardCommand request,
            CancellationToken cancellationToken)
        {
            var existing = await unitOfWork.AdminDashboardMetrics.GetByDateAsync(request.MetricDate);
            if (existing is not null)
                return ApiResponseDto<AdminDashboardResponseDto>.Fail("এই তারিখের জন্য ড্যাশবোর্ড মেট্রিক ইতিমধ্যে বিদ্যমান।");

            var metric = new AdminDashboardMetric
            {
                MetricDate = request.MetricDate,
                ActiveDoctors = request.TotalDoctors,
                PendingVerifications = request.PendingVerifications,
                TotalAppointmentsToday = request.TodayAppointments,
                PlatformRevenueToday = request.TodayRevenue,
                MonthlyPlatformRevenue = request.MonthlyRevenue,
            };

            await unitOfWork.AdminDashboardMetrics.AddAsync(metric, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = mapper.Map<AdminDashboardResponseDto>(metric);
            return ApiResponseDto<AdminDashboardResponseDto>.Ok(dto, "Admin ড্যাশবোর্ড মেট্রিক সফলভাবে তৈরি হয়েছে।");
        }
    }

}
