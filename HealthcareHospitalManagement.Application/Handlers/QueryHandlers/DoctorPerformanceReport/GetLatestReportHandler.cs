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
    public class GetLatestReportHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetLatestReportQuery, ApiResponseDto<DoctorPerformanceReportDto>>
    {
        public async Task<ApiResponseDto<DoctorPerformanceReportDto>> Handle(
            GetLatestReportQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorPerformanceReports.GetLatestReportAsync(request.DoctorId);
            if (entity is null)
                return ApiResponseDto<DoctorPerformanceReportDto>.FailResponse("No report found for this doctor.");

            return ApiResponseDto<DoctorPerformanceReportDto>.SuccessResponse(
                mapper.Map<DoctorPerformanceReportDto>(entity));
        }
    }

}
