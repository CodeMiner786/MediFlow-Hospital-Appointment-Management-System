using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorPerformanceReport;
using HealthcareHospitalManagement.Application.Helpers.Stream;
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
    public class GetReportsByPeriodHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetReportsByPeriodQuery, ApiResponseDto<PagedResultDto<DoctorPerformanceReportDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorPerformanceReportDto>>> Handle(
            GetReportsByPeriodQuery request, CancellationToken ct)
        {
            var stream = uow.DoctorPerformanceReports.GetReportsByPeriodStream(request.DoctorId, request.PeriodType);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorPerformanceReport, DoctorPerformanceReportDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorPerformanceReportDto>>.SuccessResponse(paged);
        }
    }

}
