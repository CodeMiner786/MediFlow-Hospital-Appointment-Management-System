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
    public class GetPatientGrowthReportQueryHandler
    : IRequestHandler<GetPatientGrowthReportQuery, ApiResponse<List<PatientGrowthResponseDto>>>
    {
        private readonly IAnalyticsNotificationUnitOfWork _unitOfWork;

        public GetPatientGrowthReportQueryHandler(IAnalyticsNotificationUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<ApiResponse<List<PatientGrowthResponseDto>>> Handle(
            GetPatientGrowthReportQuery query,
            CancellationToken cancellationToken)
        {
            var dto = query.RequestDto;

            if (dto.DateFrom > dto.DateTo)
                return ApiResponse<List<PatientGrowthResponseDto>>.FailResponse(
                    "DateFrom cannot be greater than DateTo.");

            var startDate = DateOnly.FromDateTime(dto.DateFrom);
            var endDate = DateOnly.FromDateTime(dto.DateTo);
            var results = new List<PatientGrowthResponseDto>();
            int cumulative = 0;

            // DashboardMetric এর NewPatients + TotalPatients থেকে growth calculate করা হচ্ছে
            await foreach (var metric in _unitOfWork.DashboardMetrics
                               .GetMetricsByDateRangeStream(startDate, endDate)
                               .WithCancellation(cancellationToken))
            {
                cumulative += metric.NewPatients;
                results.Add(new PatientGrowthResponseDto
                {
                    Date = metric.MetricDate.ToDateTime(TimeOnly.MinValue),
                    NewRegistrations = metric.NewPatients,
                    TotalPatients = metric.TotalPatients
                });
            }

            return ApiResponse<List<PatientGrowthResponseDto>>.SuccessResponse(
                results,
                "Patient growth report retrieved successfully.");
        }
    }

}
