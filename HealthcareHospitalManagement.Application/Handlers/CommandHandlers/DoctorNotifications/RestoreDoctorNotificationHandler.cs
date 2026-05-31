using HealthcareHospitalManagement.Application.Commands.DoctorNotifications;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorNotifications
{
    public class RestoreDoctorNotificationHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<RestoreDoctorNotificationCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            RestoreDoctorNotificationCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorNotifications.GetByIdAsync(request.NotificationId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Notification not found.");

            await uow.DoctorNotifications.RestoreAsync(request.NotificationId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Notification restored successfully.");
        }
    }

}
