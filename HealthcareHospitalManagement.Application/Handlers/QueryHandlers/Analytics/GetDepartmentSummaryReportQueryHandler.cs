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
    public class GetDepartmentSummaryReportQueryHandler
    : IRequestHandler<GetDepartmentSummaryReportQuery, ApiResponse<List<DepartmentSummaryResponseDto>>>
    {
        private readonly IAnalyticsNotificationUnitOfWork _unitOfWork;

        public GetDepartmentSummaryReportQueryHandler(IAnalyticsNotificationUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<ApiResponse<List<DepartmentSummaryResponseDto>>> Handle(
            GetDepartmentSummaryReportQuery query,
            CancellationToken cancellationToken)
        {
            var dto = query.RequestDto;

            if (dto.DateFrom > dto.DateTo)
                return ApiResponse<List<DepartmentSummaryResponseDto>>.FailResponse(
                    "DateFrom cannot be greater than DateTo.");

            // ── বাস্তবে IDepartmentRepository.GetSummaryAsync() call হবে ──
            var results = new List<DepartmentSummaryResponseDto>();

            return await Task.FromResult(
                ApiResponse<List<DepartmentSummaryResponseDto>>.SuccessResponse(
                    results,
                    "Department summary report retrieved successfully."));
        }
    }

}
