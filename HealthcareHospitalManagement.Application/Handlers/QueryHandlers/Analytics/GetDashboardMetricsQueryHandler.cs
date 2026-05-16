using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Analytics;
using HealthcareHospitalManagement.Application.Queries.Analytics;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;



    namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Analytics
    {
        public class GetDashboardMetricsQueryHandler(IAnalyticsNotificationUnitOfWork unitOfWork, IMapper mapper)
            : IRequestHandler<GetDashboardMetricsQuery, ApiResponse<List<DashboardMetricResponseDto>>>
        {
        public async Task<ApiResponse<List<DashboardMetricResponseDto>>> Handle(
GetDashboardMetricsQuery query,
CancellationToken cancellationToken)
        {
            var responseDtos = new List<DashboardMetricResponseDto>();

            if (query.EndDate is null)
            {
                var metric = await unitOfWork.DashboardMetrics.GetByDateAsync(query.StartDate);
                if (metric is null)
                    return ApiResponse<List<DashboardMetricResponseDto>>.FailResponse(
                        $"No dashboard metric found for {query.StartDate}.");

                responseDtos.Add(mapper.Map<DashboardMetricResponseDto>(metric));
            }
            else
            {
                await foreach (var metric in unitOfWork.DashboardMetrics
                    .GetMetricsByDateRangeStream(query.StartDate, query.EndDate.Value))
                {
                    responseDtos.Add(mapper.Map<DashboardMetricResponseDto>(metric));
                }

                if (responseDtos.Count == 0)
                    return ApiResponse<List<DashboardMetricResponseDto>>.FailResponse(
                        $"No dashboard metrics found between {query.StartDate} and {query.EndDate}.");
            }

            return ApiResponse<List<DashboardMetricResponseDto>>.SuccessResponse(
                responseDtos,
                "Dashboard metrics retrieved successfully.");
        }

    }
}

