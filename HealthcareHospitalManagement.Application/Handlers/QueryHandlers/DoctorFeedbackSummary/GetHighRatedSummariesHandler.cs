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
    public class GetHighRatedSummariesHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetHighRatedSummariesQuery, ApiResponseDto<PagedResultDto<DoctorFeedbackSummaryDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorFeedbackSummaryDto>>> Handle(
            GetHighRatedSummariesQuery request, CancellationToken ct)
        {
            var stream = uow.DoctorFeedbackSummaries.GetHighRatedSummariesStream(request.MinRating);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorFeedbackSummary, DoctorFeedbackSummaryDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorFeedbackSummaryDto>>.SuccessResponse(paged);
        }
    }

}
