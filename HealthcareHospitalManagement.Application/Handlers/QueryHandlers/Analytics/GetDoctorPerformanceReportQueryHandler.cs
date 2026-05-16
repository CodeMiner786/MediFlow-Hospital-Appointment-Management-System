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
    public class GetDoctorPerformanceReportQueryHandler
    : IRequestHandler<GetDoctorPerformanceReportQuery, ApiResponse<List<DoctorPerformanceResponseDto>>>
    {
        private readonly IAnalyticsNotificationUnitOfWork _unitOfWork;

        public GetDoctorPerformanceReportQueryHandler(IAnalyticsNotificationUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<ApiResponse<List<DoctorPerformanceResponseDto>>> Handle(
            GetDoctorPerformanceReportQuery query,
            CancellationToken cancellationToken)
        {
            var dto = query.RequestDto;

            if (dto.DateFrom > dto.DateTo)
                return ApiResponse<List<DoctorPerformanceResponseDto>>.FailResponse(
                    "DateFrom cannot be greater than DateTo.");

            // ── Doctor-specific filter থাকলে একটিমাত্র doctor এর data ──
            // বাস্তবে IDoctorRepository.GetPerformanceAsync() call হবে
            // এখন placeholder list return করা হচ্ছে
            var results = new List<DoctorPerformanceResponseDto>();

            return await Task.FromResult(
                ApiResponse<List<DoctorPerformanceResponseDto>>.SuccessResponse(
                    results,
                    "Doctor performance report retrieved successfully."));
        }
    }

}
