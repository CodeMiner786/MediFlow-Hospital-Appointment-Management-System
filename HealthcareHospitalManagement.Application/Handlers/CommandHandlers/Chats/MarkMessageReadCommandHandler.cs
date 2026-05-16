using HealthcareHospitalManagement.Application.Commands.Chats;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
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
    public class MarkMessageReadCommandHandler(IChatFeedUnitOfWork unitOfWork)
    : IRequestHandler<MarkMessageReadCommand, ApiResponse<bool>>
    {
        private readonly IChatFeedUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ApiResponse<bool>> Handle(
            MarkMessageReadCommand command,
            CancellationToken cancellationToken)
        {
            var dto = command.RequestDto;

            var message = await _unitOfWork.ChatMessages
                .GetByIdAsync(dto.MessageId, cancellationToken);

            if (message is null)
                return ApiResponse<bool>.FailResponse("Message not found.");

            // নিজের message read mark করার দরকার নেই
            if (message.SenderId == dto.ReadByUserId)
                return ApiResponse<bool>.SuccessResponse(true, "Cannot mark your own message as read.");

            if (message.ReadAt.HasValue)
                return ApiResponse<bool>.SuccessResponse(true, "Message already marked as read.");

            message.ReadAt = DateTime.UtcNow;

            // Repository এর UpdateMessageStatusAsync ব্যবহার করো
            await _unitOfWork.ChatMessages
                .UpdateMessageStatusAsync(dto.MessageId, MessageStatus.Read, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponse<bool>.SuccessResponse(true, "Message marked as read.");
        }
    }

}
