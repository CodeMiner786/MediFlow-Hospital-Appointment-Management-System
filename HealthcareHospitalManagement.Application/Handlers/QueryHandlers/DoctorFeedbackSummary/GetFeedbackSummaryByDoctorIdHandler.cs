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
    public class GetFeedbackSummaryByDoctorIdHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetFeedbackSummaryByDoctorIdQuery, ApiResponseDto<DoctorFeedbackSummaryDto>>
    {
        public async Task<ApiResponseDto<DoctorFeedbackSummaryDto>> Handle(
            GetFeedbackSummaryByDoctorIdQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorFeedbackSummaries.GetByDoctorIdAsync(request.DoctorId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorFeedbackSummaryDto>.FailResponse(
                    "Feedback summary not found for this doctor.");

            return ApiResponseDto<DoctorFeedbackSummaryDto>.SuccessResponse(
                mapper.Map<DoctorFeedbackSummaryDto>(entity));
        }
    }

}
