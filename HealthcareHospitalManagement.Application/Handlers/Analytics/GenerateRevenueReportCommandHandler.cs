using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Analytics;
using HealthcareHospitalManagement.Application.DTOs.Analytics;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Entities.Analytics;
using HealthcareHospitalManagement.Domain.Enums.Analytics;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.Analytics
{
    public class GenerateRevenueReportCommandHandler
    : IRequestHandler<GenerateRevenueReportCommand, ApiResponse<RevenueReportResponseDto>>
    {
        private readonly IAnalyticsNotificationUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenerateRevenueReportCommandHandler(
            IAnalyticsNotificationUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<RevenueReportResponseDto>> Handle(
            GenerateRevenueReportCommand command,
            CancellationToken cancellationToken)
        {
            var dto = command.RequestDto;

            if (dto.DateFrom > dto.DateTo)
                return ApiResponse<RevenueReportResponseDto>.FailResponse(
                    "DateFrom cannot be greater than DateTo.");

            // ── Date range এর metrics stream করে নিয়ে আসো ───────────────
            var startDate = DateOnly.FromDateTime(dto.DateFrom);
            var endDate = DateOnly.FromDateTime(dto.DateTo);

            var dailyBreakdown = new List<DailyRevenueDto>();
            decimal totalRevenue = 0;

            await foreach (var metric in _unitOfWork.DashboardMetrics
                               .GetMetricsByDateRangeStream(startDate, endDate)
                               .WithCancellation(cancellationToken))
            {
                totalRevenue += metric.TotalRevenue;
                dailyBreakdown.Add(new DailyRevenueDto
                {
                    Date = metric.MetricDate.ToDateTime(TimeOnly.MinValue),
                    Revenue = metric.TotalRevenue,
                    TransactionCount = metric.TotalAppointments + metric.AmbulanceBookings + metric.LabTestsOrdered
                });
            }

            // ── Report entity archive হিসেবে save করো ───────────────────
            var reportEntity = new ReportEntity
            {
                ReportTitle = $"Revenue Report {dto.DateFrom:dd MMM yyyy} – {dto.DateTo:dd MMM yyyy}",
                ReportType = ReportType.Financial,
                Category = ReportCategory.Revenue,
                FromDate = dto.DateFrom,
                ToDate = dto.DateTo,
                GeneratedAt = DateTime.UtcNow,
                GeneratedBy = "System"
            };

            await _unitOfWork.Reports.AddAsync(reportEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new RevenueReportResponseDto
            {
                DateFrom = dto.DateFrom,
                DateTo = dto.DateTo,
                TotalRevenue = totalRevenue,
                DailyBreakdown = dailyBreakdown
            };

            return ApiResponse<RevenueReportResponseDto>.SuccessResponse(
                response,
                "Revenue report generated successfully.");
        }
    }

}
