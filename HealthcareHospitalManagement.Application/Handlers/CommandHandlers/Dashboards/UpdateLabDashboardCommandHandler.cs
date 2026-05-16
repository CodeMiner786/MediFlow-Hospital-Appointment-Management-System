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
    public sealed class UpdateLabDashboardCommandHandler(
        IDashboardUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<UpdateLabDashboardCommand, ApiResponseDto<LabDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<LabDashboardResponseDto>> Handle(
            UpdateLabDashboardCommand request,
            CancellationToken cancellationToken)
        {
            var metric = await unitOfWork.LabDashboardMetrics.GetByIdAsync(request.Id, cancellationToken);
            if (metric is null)
                return ApiResponseDto<LabDashboardResponseDto>.Fail("Lab ড্যাশবোর্ড মেট্রিক পাওয়া যায়নি।");

            if (request.PendingOrders.HasValue) metric.PendingOrders = request.PendingOrders.Value;
            if (request.CompletedToday.HasValue) metric.CompletedOrders = request.CompletedToday.Value;
            if (request.TodayRevenue.HasValue) metric.TodayRevenue = request.TodayRevenue.Value;
            if (request.MonthlyRevenue.HasValue) metric.MonthlyRevenue = request.MonthlyRevenue.Value;
            if (request.WalletBalance.HasValue) metric.PendingPayments = request.WalletBalance.Value;

            await unitOfWork.LabDashboardMetrics.UpdateAsync(metric, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = mapper.Map<LabDashboardResponseDto>(metric);
            return ApiResponseDto<LabDashboardResponseDto>.Ok(dto, "Lab ড্যাশবোর্ড মেট্রিক সফলভাবে আপডেট হয়েছে।");
        }
    }
}
