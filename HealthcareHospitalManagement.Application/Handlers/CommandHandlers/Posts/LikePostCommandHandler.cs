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
    public class LikePostCommandHandler : IRequestHandler<LikePostCommand, ApiResponseDto<bool>>
    {
        private readonly IFeedUnitOfWork _unitOfWork;

        public LikePostCommandHandler(IFeedUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDto<bool>> Handle(
            LikePostCommand request,
            CancellationToken cancellationToken)
        {
            var interaction = await _unitOfWork.PostInteractions
                .GetByPostIdAsync(request.Dto.PostId, cancellationToken);

            if (interaction is null)
                return ApiResponseDto<bool>.Fail("Post not found.");

            // Toggle: আগে like করা থাকলে unlike করব
            var existingLike = await _unitOfWork.PostLikes
                .GetByInteractionAndUserAsync(interaction.Id, request.Dto.UserId, cancellationToken);

            if (existingLike is not null)
            {
                _unitOfWork.PostLikes.Remove(existingLike);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ApiResponseDto<bool>.Ok(false, "Post unliked.");
            }

            var like = new PostLike
            {
                PostInteractionId = interaction.Id,
                UserId = request.Dto.UserId
            };

            await _unitOfWork.PostLikes.AddAsync(like, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponseDto<bool>.Ok(true, "Post liked.");
        }
    }

}
