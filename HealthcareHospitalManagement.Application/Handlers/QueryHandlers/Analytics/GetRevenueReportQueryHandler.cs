using HealthcareHospitalManagement.Application.DTOs.Analytics;
using HealthcareHospitalManagement.Application.Queries.Analytics;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Analytics
{
    public class GetRevenueReportQueryHandler
    : IRequestHandler<GetRevenueReportQuery, ApiResponse<RevenueReportResponseDto>>
    {
        private readonly IAnalyticsNotificationUnitOfWork _unitOfWork;

        public GetRevenueReportQueryHandler(IAnalyticsNotificationUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<ApiResponse<RevenueReportResponseDto>> Handle(
            GetRevenueReportQuery query,
            CancellationToken cancellationToken)
        {
            var dto = query.RequestDto;

            if (dto.DateFrom > dto.DateTo)
                return ApiResponse<RevenueReportResponseDto>.FailResponse(
                    "DateFrom cannot be greater than DateTo.");

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

            var response = new RevenueReportResponseDto
            {
                DateFrom = dto.DateFrom,
                DateTo = dto.DateTo,
                TotalRevenue = totalRevenue,
                DailyBreakdown = dailyBreakdown
            };

            return ApiResponse<RevenueReportResponseDto>.SuccessResponse(
                response,
                "Revenue report retrieved successfully.");
        }
    }

}
