using HealthcareHospitalManagement.Application.Commands.Feeds;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Domain.Entities.Feed;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Feeds
{
    // 🔹 Primary constructor ব্যবহার করা হলো
    public class LikeFeedItemCommandHandler(IFeedUnitOfWork unitOfWork)
        : IRequestHandler<LikeFeedItemCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            LikeFeedItemCommand request,
            CancellationToken cancellationToken)
        {
            var feedItem = await unitOfWork.FeedItems.GetByIdAsync(request.Dto.FeedItemId, cancellationToken);
            if (feedItem is null)
                return ApiResponseDto<bool>.Fail("Feed item not found.");

            // Toggle: আগে like করা থাকলে unlike করব
            var existingLike = await unitOfWork.FeedItemLikes
                .GetByFeedItemAndUserAsync(request.Dto.FeedItemId, request.Dto.UserId, cancellationToken);

            if (existingLike is not null)
            {
                // 🔹 cancellationToken forward করা হলো
                await unitOfWork.FeedItemLikes.RemoveAsync(existingLike, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return ApiResponseDto<bool>.Ok(false, "Feed item unliked.");
            }

            var like = new FeedItemLike
            {
                FeedItemId = request.Dto.FeedItemId,
                UserId = request.Dto.UserId
            };

            await unitOfWork.FeedItemLikes.AddAsync(like, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponseDto<bool>.Ok(true, "Feed item liked.");
        }
    }
}
