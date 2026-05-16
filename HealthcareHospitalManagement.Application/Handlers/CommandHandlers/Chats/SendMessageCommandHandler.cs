using HealthcareHospitalManagement.Application.Commands.Chats;
using HealthcareHospitalManagement.Application.DTOs.Chat;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Entities.Chat;
using HealthcareHospitalManagement.Domain.Enums.Chat;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Chats
{
    public class SendMessageCommandHandler(IChatFeedUnitOfWork unitOfWork)
    : IRequestHandler<SendMessageCommand, ApiResponse<ChatMessageDetailResponseDto>>
    {
        private readonly IChatFeedUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ApiResponse<ChatMessageDetailResponseDto>> Handle(
            SendMessageCommand command,
            CancellationToken cancellationToken)
        {
            var dto = command.RequestDto;

            // ── ১. Conversation আছে কিনা চেক করো ─────────────────────────
            var conversation = await _unitOfWork.Conversations
                .GetByIdAsync(dto.ConversationId, cancellationToken);

            if (conversation is null)
                return ApiResponse<ChatMessageDetailResponseDto>.FailResponse(
                    "Conversation not found.");

            if (!conversation.IsActive)
                return ApiResponse<ChatMessageDetailResponseDto>.FailResponse(
                    "Conversation is no longer active.");

            // ── ২. Content validation ──────────────────────────────────────
            if (string.IsNullOrWhiteSpace(dto.Content) && string.IsNullOrWhiteSpace(dto.AttachmentUrl))
                return ApiResponse<ChatMessageDetailResponseDto>.FailResponse(
                    "Message must have content or an attachment.");

            // ── ৩. Message তৈরি করো ───────────────────────────────────────
            var message = new ChatMessage
            {
                ConversationId = dto.ConversationId,
                SenderId = dto.SenderId,
                SenderType = dto.SenderType,
                Content = dto.Content,
                Status = MessageStatus.Sent,
                AttachmentUrl = dto.AttachmentUrl,
                AttachmentType = dto.AttachmentType,
                AttachmentSizeBytes = dto.AttachmentSizeBytes,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.ChatMessages.AddAsync(message, cancellationToken);

            // ── ৪. Conversation এর last message preview update করো ─────────
            conversation.LastMessagePreview = string.IsNullOrWhiteSpace(dto.Content)
                ? $"📎 {dto.AttachmentType ?? "Attachment"}"
                : dto.Content.Length > 60
                    ? dto.Content[..60] + "..."
                    : dto.Content;

            conversation.LastMessageAt = DateTime.UtcNow;
            await _unitOfWork.Conversations.UpdateAsync(conversation, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // ── ৫. Sender info navigation property থেকে নাও ─────────────
            var senderName = string.Empty;
            var senderImage = (string?)null;

            var sender = message.Sender;
            if (sender is not null)
            {
                senderName = $"{sender.FirstName} {sender.LastName}".Trim();
                senderImage = sender.ProfileImageUrl;
            }

            var responseDto = new ChatMessageDetailResponseDto
            {
                Id = message.Id,
                ConversationId = message.ConversationId,
                SenderId = message.SenderId,
                SenderName = senderName,
                SenderImageUrl = senderImage,
                SenderType = message.SenderType.ToString(),
                Content = message.Content,
                Status = message.Status,
                AttachmentUrl = message.AttachmentUrl,
                AttachmentType = message.AttachmentType,
                AttachmentSizeBytes = message.AttachmentSizeBytes,
                SentAt = message.CreatedAt,
                DeliveredAt = message.DeliveredAt,
                ReadAt = message.ReadAt
            };

            return ApiResponse<ChatMessageDetailResponseDto>.SuccessResponse(
                responseDto, "Message sent successfully.");
        }
    }

}
