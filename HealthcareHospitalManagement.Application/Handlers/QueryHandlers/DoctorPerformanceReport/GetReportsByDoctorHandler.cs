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
    public class GetReportsByDoctorHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetReportsByDoctorQuery, ApiResponseDto<PagedResultDto<DoctorPerformanceReportDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorPerformanceReportDto>>> Handle(
            GetReportsByDoctorQuery request, CancellationToken ct)
        {
            var stream = uow.DoctorPerformanceReports.GetReportsByDoctorStream(request.DoctorId);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorPerformanceReport, DoctorPerformanceReportDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorPerformanceReportDto>>.SuccessResponse(paged);
        }
    }

}
