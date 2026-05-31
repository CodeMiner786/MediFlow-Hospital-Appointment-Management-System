using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorFeedbackSummary;
using HealthcareHospitalManagement.Application.Helpers.Stream;
using HealthcareHospitalManagement.Application.Queries.DoctorFeedbackSummary;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.DoctorFeedbackSummary
{
    public class GetAllFeedbackSummariesHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetAllFeedbackSummariesQuery, ApiResponseDto<PagedResultDto<DoctorFeedbackSummaryDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorFeedbackSummaryDto>>> Handle(
            GetAllFeedbackSummariesQuery request, CancellationToken ct)
        {
            var all = await uow.DoctorFeedbackSummaries.GetAllAsync(ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorFeedbackSummary, DoctorFeedbackSummaryDto>(
                            [.. all], request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorFeedbackSummaryDto>>.SuccessResponse(paged);
        }
    }

}
