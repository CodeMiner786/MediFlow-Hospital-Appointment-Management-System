using HealthcareHospitalManagement.Application.Queries.Chats;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Chats
{
    public class GetUnreadCountQueryHandler(IChatFeedUnitOfWork unitOfWork)
    : IRequestHandler<GetUnreadCountQuery, ApiResponse<int>>
    {
        private readonly IChatFeedUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ApiResponse<int>> Handle(
            GetUnreadCountQuery query,
            CancellationToken cancellationToken)
        {
            var totalUnread = 0;

            // User এর সব conversation এর unread count sum করো
            await foreach (var conversation in _unitOfWork.Conversations
                               .GetUserConversationsStream(query.UserId)
                               .WithCancellation(cancellationToken))
            {
                await foreach (var msg in _unitOfWork.ChatMessages
                                   .GetUnreadMessagesByConversationStream(conversation.Id)
                                   .WithCancellation(cancellationToken))
                {
                    // নিজের পাঠানো message count করবে না
                    if (msg.SenderId != query.UserId)
                        totalUnread++;
                }
            }

            return ApiResponse<int>.SuccessResponse(totalUnread,
                $"You have {totalUnread} unread message(s).");
        }
    }

}
