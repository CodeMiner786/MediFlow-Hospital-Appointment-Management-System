using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Notifications;
using HealthcareHospitalManagement.Application.DTOs.Notification;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Entities.Notification; // ✅ সঠিক entity namespace
using HealthcareHospitalManagement.Domain.Interfaces.Notification;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Notifications
{
    public class SendNotificationCommandHandler(IUserNotificationRepository notificationRepository, IMapper mapper)
        : IRequestHandler<SendNotificationCommand, ApiResponse<UserNotificationResponseDto>>
    {
        public async Task<ApiResponse<UserNotificationResponseDto>> Handle(
            SendNotificationCommand command,
            CancellationToken cancellationToken)
        {
            var notification = new UserNotification
            {
                UserId = command.RequestDto.UserId,
                Title = command.RequestDto.Title,
                Message = command.RequestDto.Message,
                ActionUrl = command.RequestDto.ActionUrl,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await notificationRepository.AddAsync(notification, cancellationToken);
            await notificationRepository.SaveChangesAsync(cancellationToken);

            var responseDto = mapper.Map<UserNotificationResponseDto>(notification);

            return ApiResponse<UserNotificationResponseDto>.SuccessResponse(
                responseDto,
                "Notification sent successfully.");
        }
    }
}
