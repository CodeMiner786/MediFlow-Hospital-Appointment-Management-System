using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.DoctorPerformanceReport;
using HealthcareHospitalManagement.Application.Queries.DoctorPerformanceReport;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.DoctorPerformanceReport
{
    public class GetReportByDateRangeHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetReportByDateRangeQuery, ApiResponseDto<DoctorPerformanceReportDto>>
    {
        public async Task<ApiResponseDto<DoctorPerformanceReportDto>> Handle(
            GetReportByDateRangeQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorPerformanceReports.GetReportByDateRangeAsync(
                request.DoctorId, request.FromDate, request.ToDate);

            if (entity is null)
                return ApiResponseDto<DoctorPerformanceReportDto>.FailResponse(
                    "No report found for the given date range.");

            return ApiResponseDto<DoctorPerformanceReportDto>.SuccessResponse(
                mapper.Map<DoctorPerformanceReportDto>(entity));
        }
    }


}
