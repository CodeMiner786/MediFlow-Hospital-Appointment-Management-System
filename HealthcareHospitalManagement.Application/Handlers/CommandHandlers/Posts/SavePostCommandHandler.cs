using HealthcareHospitalManagement.Application.Commands.Posts;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Posts
{
    public class SavePostCommandHandler : IRequestHandler<SavePostCommand, ApiResponseDto<bool>>
    {
        private readonly IFeedUnitOfWork _unitOfWork;

        public SavePostCommandHandler(IFeedUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDto<bool>> Handle(
            SavePostCommand request,
            CancellationToken cancellationToken)
        {
            var interaction = await _unitOfWork.PostInteractions
                .GetByPostIdAsync(request.Dto.PostId, cancellationToken);

            if (interaction is null)
                return ApiResponseDto<bool>.Fail("Post not found.");

            // Toggle: আগে save করা থাকলে unsave করব
            var existingSave = await _unitOfWork.PostSaves
                .GetByInteractionAndUserAsync(interaction.Id, request.Dto.UserId, cancellationToken);

            if (existingSave is not null)
            {
                _unitOfWork.PostSaves.Remove(existingSave);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ApiResponseDto<bool>.Ok(false, "Post unsaved.");
            }

            var save = new PostSave
            {
                PostInteractionId = interaction.Id,
                UserId = request.Dto.UserId,
                SavedAt = DateTime.UtcNow
            };

            await _unitOfWork.PostSaves.AddAsync(save, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponseDto<bool>.Ok(true, "Post saved.");
        }
    }

}
