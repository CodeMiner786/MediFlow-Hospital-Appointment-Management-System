using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.DoctorFeedbackSummary;
using HealthcareHospitalManagement.Application.DTOs.DoctorFeedbackSummary;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorFeedbackSummary
{
    public class UpdateDoctorFeedbackSummaryHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateDoctorFeedbackSummaryCommand, ApiResponseDto<DoctorFeedbackSummaryDto>>
    {
        public async Task<ApiResponseDto<DoctorFeedbackSummaryDto>> Handle(
            UpdateDoctorFeedbackSummaryCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorFeedbackSummaries.GetByIdAsync(request.SummaryId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorFeedbackSummaryDto>.FailResponse("Feedback summary not found.");

            mapper.Map(request.Dto, entity);
            entity.LastUpdatedAt = DateTime.UtcNow;

            await uow.DoctorFeedbackSummaries.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorFeedbackSummaryDto>.SuccessResponse(
                mapper.Map<DoctorFeedbackSummaryDto>(entity), "Feedback summary updated successfully.");
        }
    }

}
