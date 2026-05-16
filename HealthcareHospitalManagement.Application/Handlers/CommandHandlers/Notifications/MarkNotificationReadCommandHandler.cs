using HealthcareHospitalManagement.Application.Commands.Notifications;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Interfaces.Notification;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Notifications
{
    public class MarkNotificationReadCommandHandler(IUserNotificationRepository notificationRepository)
        : IRequestHandler<MarkNotificationReadCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(
            MarkNotificationReadCommand command,
            CancellationToken cancellationToken)
        {
            var notification = await notificationRepository.GetByIdAsync(
                command.RequestDto.NotificationId,
                cancellationToken);

            if (notification is null)
                return ApiResponse<bool>.FailResponse("Notification not found.");

            if (notification.IsRead)
                return ApiResponse<bool>.SuccessResponse(true, "Notification was already marked as read.");

            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;

            // 🔹 এখন cancellationToken pass করা হচ্ছে
            await notificationRepository.UpdateAsync(notification, cancellationToken);

            // 🔹 SaveChangesAsync method আমরা IUserNotificationRepository এ যোগ করেছি
            await notificationRepository.SaveChangesAsync(cancellationToken);

            return ApiResponse<bool>.SuccessResponse(true, "Notification marked as read successfully.");
        }
    }
}
