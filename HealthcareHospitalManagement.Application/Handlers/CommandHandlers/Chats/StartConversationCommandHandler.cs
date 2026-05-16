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
    public class StartConversationCommandHandler(IChatFeedUnitOfWork unitOfWork)
    : IRequestHandler<StartConversationCommand, ApiResponse<ConversationSummaryResponseDto>>
    {
        private readonly IChatFeedUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ApiResponse<ConversationSummaryResponseDto>> Handle(
            StartConversationCommand command,
            CancellationToken cancellationToken)
        {
            var dto = command.RequestDto;

            // ── ১. ইতিমধ্যে conversation আছে কিনা চেক করো ────────────────
            var existing = await _unitOfWork.Conversations
                .GetConversationBetweenUsersAsync(dto.InitiatorUserId, dto.RecipientUserId);

            if (existing is not null)
            {
                var existingDto = BuildSummary(existing, dto.InitiatorUserId);
                return ApiResponse<ConversationSummaryResponseDto>.SuccessResponse(
                    existingDto, "Conversation already exists.");
            }

            // ── ২. নতুন conversation তৈরি করো ────────────────────────────
            var conversation = new Conversation
            {
                Type = dto.ConversationType,
                IsActive = true,
                ParticipantAId = dto.InitiatorUserId,
                ParticipantAType = ChatParticipantType.User,
                ParticipantBId = dto.RecipientUserId,
                ParticipantBType = ChatParticipantType.User,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Conversations.AddAsync(conversation, cancellationToken);

            // ── ৩. Initial message থাকলে save করো ────────────────────────
            if (!string.IsNullOrWhiteSpace(dto.InitialMessage))
            {
                var message = new ChatMessage
                {
                    ConversationId = conversation.Id,
                    SenderId = dto.InitiatorUserId,
                    SenderType = ChatParticipantType.User,
                    Content = dto.InitialMessage,
                    Status = MessageStatus.Sent,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.ChatMessages.AddAsync(message, cancellationToken);

                conversation.LastMessagePreview = dto.InitialMessage;
                conversation.LastMessageAt = DateTime.UtcNow;
                await _unitOfWork.Conversations.UpdateAsync(conversation, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<ConversationSummaryResponseDto>.SuccessResponse(
                BuildSummary(conversation, dto.InitiatorUserId),
                "Conversation started successfully.");
        }

        // ── Helper ────────────────────────────────────────────────────────
        private static ConversationSummaryResponseDto BuildSummary(
            Conversation conversation,
            Guid currentUserId)
        {
            var isInitiator = conversation.ParticipantAId == currentUserId;
            var otherUser = isInitiator ? conversation.ParticipantB : conversation.ParticipantA;
            var otherId = isInitiator ? conversation.ParticipantBId : conversation.ParticipantAId;

            return new ConversationSummaryResponseDto
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
                IsLastMessageMine = true,
                UnreadCount = 0,
                IsActive = conversation.IsActive
            };
        }
    }

}
