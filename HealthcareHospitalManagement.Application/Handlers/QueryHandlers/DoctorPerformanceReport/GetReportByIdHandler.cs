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
    public class GetReportByIdHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetReportByIdQuery, ApiResponseDto<DoctorPerformanceReportDto>>
    {
        public async Task<ApiResponseDto<DoctorPerformanceReportDto>> Handle(
            GetReportByIdQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorPerformanceReports.GetByIdAsync(request.ReportId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorPerformanceReportDto>.FailResponse("Report not found.");

            return ApiResponseDto<DoctorPerformanceReportDto>.SuccessResponse(
                mapper.Map<DoctorPerformanceReportDto>(entity));
        }
    }

}
