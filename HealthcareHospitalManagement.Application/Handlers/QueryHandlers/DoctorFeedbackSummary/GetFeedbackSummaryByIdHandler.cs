using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.DoctorFeedbackSummary;
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
    public class GetFeedbackSummaryByIdHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetFeedbackSummaryByIdQuery, ApiResponseDto<DoctorFeedbackSummaryDto>>
    {
        public async Task<ApiResponseDto<DoctorFeedbackSummaryDto>> Handle(
            GetFeedbackSummaryByIdQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorFeedbackSummaries.GetByIdAsync(request.SummaryId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorFeedbackSummaryDto>.FailResponse("Feedback summary not found.");

            return ApiResponseDto<DoctorFeedbackSummaryDto>.SuccessResponse(
                mapper.Map<DoctorFeedbackSummaryDto>(entity));
        }
    }

}
