using HealthcareHospitalManagement.Application.DTOs.Chat;
using HealthcareHospitalManagement.Application.Queries.Chats;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Entities.Chat;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Chats
{
    public class GetConversationsQueryHandler(IChatFeedUnitOfWork unitOfWork)
    : IRequestHandler<GetConversationsQuery, ApiResponse<List<ConversationSummaryResponseDto>>>
    {
        private readonly IChatFeedUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ApiResponse<List<ConversationSummaryResponseDto>>> Handle(
            GetConversationsQuery query,
            CancellationToken cancellationToken)
        {
            var dto = query.RequestDto;
            var results = new List<ConversationSummaryResponseDto>();

            // Recent conversations stream (last message time অনুযায়ী sorted)
            await foreach (var conversation in _unitOfWork.Conversations
                               .GetRecentConversationsStream(dto.UserId)
                               .WithCancellation(cancellationToken))
            {
                // IsActive filter
                if (dto.IsActive.HasValue && conversation.IsActive != dto.IsActive.Value)
                    continue;

                // Unread count calculate করো
                var unreadCount = 0;
                await foreach (var msg in _unitOfWork.ChatMessages
                                   .GetUnreadMessagesByConversationStream(conversation.Id)
                                   .WithCancellation(cancellationToken))
                {
                    if (msg.SenderId != dto.UserId)
                        unreadCount++;
                }

                var isInitiator = conversation.ParticipantAId == dto.UserId;
                var otherUser = isInitiator ? conversation.ParticipantB : conversation.ParticipantA;
                var otherId = isInitiator ? conversation.ParticipantBId : conversation.ParticipantAId;

                results.Add(new ConversationSummaryResponseDto
                {
                    Id = conversation.Id,
                    ConversationType = conversation.Type,
                    OtherUserId = otherId,
                    OtherUserName = otherUser is not null
                                            ? $"{otherUser.FirstName} {otherUser.LastName}".Trim()
                                            : string.Empty,
                    OtherUserImageUrl = otherUser?.ProfileImageUrl,
                    OtherUserRole = otherUser?.Role.ToString() ?? string.Empty,
                    LastMessage = conversation.LastMessagePreview,
                    LastMessageAt = conversation.LastMessageAt,
                    IsLastMessageMine = conversation.LastMessageAt.HasValue &&
                                        IsLastMessageFromCurrentUser(conversation, dto.UserId),
                    UnreadCount = unreadCount,
                    IsActive = conversation.IsActive
                });
            }

            // Manual paging
            var paged = results
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();

            return ApiResponse<List<ConversationSummaryResponseDto>>.SuccessResponse(
                paged, $"{paged.Count} conversation(s) retrieved.");
        }

        private static bool IsLastMessageFromCurrentUser(Conversation conversation, Guid userId)
            => conversation.ParticipantAId == userId || conversation.ParticipantBId == userId;
    }

}
