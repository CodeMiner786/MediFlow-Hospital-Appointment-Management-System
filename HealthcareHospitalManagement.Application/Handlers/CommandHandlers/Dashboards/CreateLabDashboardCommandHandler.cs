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
    public sealed class CreateLabDashboardCommandHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<CreateLabDashboardCommand, ApiResponseDto<LabDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<LabDashboardResponseDto>> Handle(
            CreateLabDashboardCommand request,
            CancellationToken cancellationToken)
        {
            var existing = await unitOfWork.LabDashboardMetrics.GetByLabAndDateAsync(request.LabProfileId, request.MetricDate);
            if (existing is not null)
                return ApiResponseDto<LabDashboardResponseDto>.Fail("এই ল্যাবের এই তারিখের মেট্রিক ইতিমধ্যে বিদ্যমান।");

            var metric = new LabDashboardMetric
            {
                LabProfileId = request.LabProfileId,
                MetricDate = request.MetricDate,
                PendingOrders = request.PendingOrders,
                TodayRevenue = request.TodayRevenue,
                MonthlyRevenue = request.MonthlyRevenue,
                CompletedOrders = request.CompletedToday,
            };

            await unitOfWork.LabDashboardMetrics.AddAsync(metric, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = mapper.Map<LabDashboardResponseDto>(metric);
            return ApiResponseDto<LabDashboardResponseDto>.Ok(dto, "Lab ড্যাশবোর্ড মেট্রিক সফলভাবে তৈরি হয়েছে।");
        }
    }

}
