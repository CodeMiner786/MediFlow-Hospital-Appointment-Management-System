using HealthcareHospitalManagement.Application.Commands.DoctorAvailabilityLog;
using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorAvailabilityLog
{
    // প্রাইমারি কনস্ট্রাক্টরের 'repository' ভেরিয়েবলটি পুরো ক্লাসের যেকোনো জায়গায় সরাসরি ব্যবহার করা যাবে
    public sealed class DeleteDoctorAvailabilityLogCommandHandler(
        IDoctorAvailabilityLogRepository repository)
        : IRequestHandler<DeleteDoctorAvailabilityLogCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            DeleteDoctorAvailabilityLogCommand request,
            CancellationToken cancellationToken)
        {
            
            var entity = await repository.GetByIdAsync(request.LogId, cancellationToken);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse($"Log with Id '{request.LogId}' not found.");

            await repository.DeleteAsync(entity.Id, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return ApiResponseDto<bool>.SuccessResponse(true, "Log successfully deleted.");
        }
    }
}