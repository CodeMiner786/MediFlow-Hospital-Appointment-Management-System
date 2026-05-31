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
    public class UpdateSummaryStatsByDoctorHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<UpdateSummaryStatsByDoctorCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            UpdateSummaryStatsByDoctorCommand request, CancellationToken ct)
        {
            var existing = await uow.DoctorFeedbackSummaries.GetByDoctorIdAsync(request.DoctorId, ct);
            if (existing is null)
                return ApiResponseDto<bool>.FailResponse("Feedback summary not found for this doctor.");

            await uow.DoctorFeedbackSummaries.UpdateSummaryStatsAsync(
                request.DoctorId, request.NewAverage, request.TotalReviews, ct);

            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Summary stats updated successfully.");
        }
    }

}
