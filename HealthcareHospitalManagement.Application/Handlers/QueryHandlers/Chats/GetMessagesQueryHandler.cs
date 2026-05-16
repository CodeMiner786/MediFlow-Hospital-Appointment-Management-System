using HealthcareHospitalManagement.Application.DTOs.Chat;
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
    public class GetMessagesQueryHandler(IChatFeedUnitOfWork unitOfWork)
    : IRequestHandler<GetMessagesQuery, ApiResponse<PagedMessagesResponseDto>>
    {
        private readonly IChatFeedUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ApiResponse<PagedMessagesResponseDto>> Handle(
            GetMessagesQuery query,
            CancellationToken cancellationToken)
        {
            var dto = query.RequestDto;

            // Conversation আছে কিনা চেক করো
            var conversation = await _unitOfWork.Conversations
                .GetByIdAsync(dto.ConversationId, cancellationToken);

            if (conversation is null)
                return ApiResponse<PagedMessagesResponseDto>.FailResponse("Conversation not found.");

            // Stream থেকে সব messages নাও
            var allMessages = new List<ChatMessageDetailResponseDto>();

            await foreach (var msg in _unitOfWork.ChatMessages
                               .GetMessagesByConversationIdStream(dto.ConversationId)
                               .WithCancellation(cancellationToken))
            {
                var sender = msg.Sender;
                var senderName = sender is not null
                    ? $"{sender.FirstName} {sender.LastName}".Trim()
                    : string.Empty;

                allMessages.Add(new ChatMessageDetailResponseDto
                {
                    Id = msg.Id,
                    ConversationId = msg.ConversationId,
                    SenderId = msg.SenderId,
                    SenderName = senderName,
                    SenderImageUrl = sender?.ProfileImageUrl,
                    SenderType = msg.SenderType.ToString(),
                    Content = msg.Content,
                    Status = msg.Status,
                    AttachmentUrl = msg.AttachmentUrl,
                    AttachmentType = msg.AttachmentType,
                    AttachmentSizeBytes = msg.AttachmentSizeBytes,
                    SentAt = msg.CreatedAt,
                    DeliveredAt = msg.DeliveredAt,
                    ReadAt = msg.ReadAt
                });
            }

            // Newest first (messenger style) তারপর paging
            var sorted = allMessages.OrderByDescending(m => m.SentAt).ToList();
            var totalCount = sorted.Count;
            var paged = sorted
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();

            var result = new PagedMessagesResponseDto
            {
                ConversationId = dto.ConversationId,
                Messages = paged,
                TotalCount = totalCount,
                PageNumber = dto.PageNumber,
                PageSize = dto.PageSize
            };

            return ApiResponse<PagedMessagesResponseDto>.SuccessResponse(
                result, "Messages retrieved successfully.");
        }
    }

}
