using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Notification;
using HealthcareHospitalManagement.Application.Queries.Notifications;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Interfaces.Notification;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Notifications
{
    // 🔹 Primary constructor ব্যবহার করা হয়েছে
    public class GetUserNotificationsQueryHandler(IUserNotificationRepository notificationRepository, IMapper mapper)
        : IRequestHandler<GetUserNotificationsQuery, ApiResponse<PagedResultDto<UserNotificationResponseDto>>>
    {
        public async Task<ApiResponse<PagedResultDto<UserNotificationResponseDto>>> Handle(
            GetUserNotificationsQuery query,
            CancellationToken cancellationToken)
        {
            var pagedResponse = await notificationRepository.GetUserNotificationsAsync(
                query.RequestDto.UserId,
                query.RequestDto.IsRead,
                query.RequestDto.PageNumber,
                query.RequestDto.PageSize,
                cancellationToken);

            var result = pagedResponse.ToMappedPagedResult<
                Domain.Entities.Notification.UserNotification,
                UserNotificationResponseDto>(mapper);

            return ApiResponse<PagedResultDto<UserNotificationResponseDto>>.SuccessResponse(
                result,
                "User notifications retrieved successfully.");
        }
    }
}
