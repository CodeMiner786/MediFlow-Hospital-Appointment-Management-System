using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Analytics;
using HealthcareHospitalManagement.Application.Queries.Analytics;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Analytics
{
    // ✅ Primary constructor ব্যবহার করা হলো
    public class GetDashboardMetricsByDateRangeQueryHandler(
        IAnalyticsNotificationUnitOfWork unitOfWork,
        IMapper mapper)
        : IRequestHandler<GetDashboardMetricsQuery, ApiResponse<List<DashboardMetricResponseDto>>>
    {
        public async Task<ApiResponse<List<DashboardMetricResponseDto>>> Handle(
            GetDashboardMetricsQuery query,
            CancellationToken cancellationToken)
        {
            // ✅ null check
            if (query.EndDate is null)
                return ApiResponse<List<DashboardMetricResponseDto>>.FailResponse(
                    "EndDate must be provided for range query.");

            if (query.StartDate > query.EndDate.Value)
                return ApiResponse<List<DashboardMetricResponseDto>>.FailResponse(
                    "StartDate cannot be greater than EndDate.");

            var results = new List<DashboardMetricResponseDto>();

            await foreach (var metric in unitOfWork.DashboardMetrics
                               .GetMetricsByDateRangeStream(query.StartDate, query.EndDate.Value)
                               .WithCancellation(cancellationToken))
            {
                results.Add(mapper.Map<DashboardMetricResponseDto>(metric));
            }

            if (results.Count == 0)
                return ApiResponse<List<DashboardMetricResponseDto>>.FailResponse(
                    "No metrics found for the given date range.");

            return ApiResponse<List<DashboardMetricResponseDto>>.SuccessResponse(
                results,
                $"{results.Count} metric(s) retrieved successfully.");
        }
    }
}
