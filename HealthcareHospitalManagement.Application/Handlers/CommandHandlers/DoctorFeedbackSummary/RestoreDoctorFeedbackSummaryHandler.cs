using HealthcareHospitalManagement.Application.Commands.DoctorFeedbackSummary;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorFeedbackSummary
{
    public class RestoreDoctorFeedbackSummaryHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<RestoreDoctorFeedbackSummaryCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            RestoreDoctorFeedbackSummaryCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorFeedbackSummaries.GetByIdAsync(request.SummaryId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Feedback summary not found.");

            await uow.DoctorFeedbackSummaries.RestoreAsync(request.SummaryId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Feedback summary restored successfully.");
        }
    }

}
