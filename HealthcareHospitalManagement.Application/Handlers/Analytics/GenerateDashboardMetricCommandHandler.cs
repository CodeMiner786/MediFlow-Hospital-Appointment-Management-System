using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Analytics;
using HealthcareHospitalManagement.Application.DTOs.Analytics;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Entities.Analytics;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.Analytics
{
    public class GenerateDashboardMetricCommandHandler
    : IRequestHandler<GenerateDashboardMetricCommand, ApiResponse<DashboardMetricResponseDto>>
    {
        private readonly IAnalyticsNotificationUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenerateDashboardMetricCommandHandler(
            IAnalyticsNotificationUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<DashboardMetricResponseDto>> Handle(
            GenerateDashboardMetricCommand command,
            CancellationToken cancellationToken)
        {
            // ইতিমধ্যে ঐ তারিখের metric আছে কিনা চেক করো
            var existing = await _unitOfWork.DashboardMetrics.GetByDateAsync(command.MetricDate);
            if (existing is not null)
                return ApiResponse<DashboardMetricResponseDto>.FailResponse(
                    $"Dashboard metric for {command.MetricDate} already exists.");

            var metric = new DashboardMetric
            {
                MetricDate = command.MetricDate,
                // ── বাস্তব প্রজেক্টে এখানে অন্য repositories থেকে aggregate করবে ──
                // এখন default 0 দিয়ে তৈরি করা হচ্ছে
            };

            await _unitOfWork.DashboardMetrics.AddAsync(metric, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseDto = _mapper.Map<DashboardMetricResponseDto>(metric);

            return ApiResponse<DashboardMetricResponseDto>.SuccessResponse(
                responseDto,
                $"Dashboard metric for {command.MetricDate} generated successfully.");
        }
    }

}
