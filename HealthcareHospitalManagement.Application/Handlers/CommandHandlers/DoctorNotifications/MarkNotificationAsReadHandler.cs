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
    public class MarkNotificationAsReadHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<MarkNotificationAsReadCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            MarkNotificationAsReadCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorNotifications.GetByIdAsync(request.NotificationId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Notification not found.");

            await uow.DoctorNotifications.MarkAsReadAsync(request.NotificationId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Notification marked as read.");
        }
    }

}
