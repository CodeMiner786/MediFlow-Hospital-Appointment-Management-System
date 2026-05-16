using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Notification;
using HealthcareHospitalManagement.Application.Queries.Notifications;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Enums.Notification;
using HealthcareHospitalManagement.Domain.Interfaces.Notification;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Notifications
{
    // 🔹 Primary constructor ব্যবহার করা হয়েছে
    public class GetNotificationTemplatesQueryHandler(INotificationTemplateRepository templateRepository, IMapper mapper)
        : IRequestHandler<GetNotificationTemplatesQuery, ApiResponse<PagedResultDto<NotificationTemplateResponseDto>>>
    {
        public async Task<ApiResponse<PagedResultDto<NotificationTemplateResponseDto>>> Handle(
            GetNotificationTemplatesQuery query,
            CancellationToken cancellationToken)
        {
            // ——— Priority: SearchTerm → Channel → Category → All ———
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var searchResult = await templateRepository.SearchTemplatesAsync(
                    query.SearchTerm,
                    query.PageNumber,
                    query.PageSize,
                    cancellationToken);

                return ApiResponse<PagedResultDto<NotificationTemplateResponseDto>>.SuccessResponse(
                    searchResult.ToMappedPagedResult<
                        Domain.Entities.Notification.NotificationTemplate,
                        NotificationTemplateResponseDto>(mapper),
                    "Templates retrieved by search term.");
            }

            if (!string.IsNullOrWhiteSpace(query.Channel)
                && Enum.TryParse<NotificationChannel>(query.Channel, ignoreCase: true, out var channel))
            {
                var channelResult = await templateRepository.GetByChannelAsync(
                    channel,
                    query.PageNumber,
                    query.PageSize,
                    cancellationToken);

                return ApiResponse<PagedResultDto<NotificationTemplateResponseDto>>.SuccessResponse(
                    channelResult.ToMappedPagedResult<
                        Domain.Entities.Notification.NotificationTemplate,
                        NotificationTemplateResponseDto>(mapper),
                    "Templates retrieved by channel.");
            }

            if (!string.IsNullOrWhiteSpace(query.Category)
                && Enum.TryParse<NotificationCategory>(query.Category, ignoreCase: true, out var category))
            {
                var categoryResult = await templateRepository.GetByCategoryAsync(
                    category,
                    query.PageNumber,
                    query.PageSize,
                    cancellationToken);

                return ApiResponse<PagedResultDto<NotificationTemplateResponseDto>>.SuccessResponse(
                    categoryResult.ToMappedPagedResult<
                        Domain.Entities.Notification.NotificationTemplate,
                        NotificationTemplateResponseDto>(mapper),
                    "Templates retrieved by category.");
            }

            var allResult = await templateRepository.GetAllPagedAsync(
                query.PageNumber,
                query.PageSize,
                cancellationToken);

            return ApiResponse<PagedResultDto<NotificationTemplateResponseDto>>.SuccessResponse(
                allResult.ToMappedPagedResult<
                    Domain.Entities.Notification.NotificationTemplate,
                    NotificationTemplateResponseDto>(mapper),
                "All notification templates retrieved successfully.");
        }
    }
}
