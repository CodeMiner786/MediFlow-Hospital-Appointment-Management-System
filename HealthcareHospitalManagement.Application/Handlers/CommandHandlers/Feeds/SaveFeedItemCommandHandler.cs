using HealthcareHospitalManagement.Application.Commands.Feeds;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Domain.Entities.Feed;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Feeds
{
    // 🔹 Primary constructor ব্যবহার করা হলো
    public class SaveFeedItemCommandHandler(IFeedUnitOfWork unitOfWork)
        : IRequestHandler<SaveFeedItemCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            SaveFeedItemCommand request,
            CancellationToken cancellationToken)
        {
            var feedItem = await unitOfWork.FeedItems.GetByIdAsync(request.Dto.FeedItemId, cancellationToken);
            if (feedItem is null)
                return ApiResponseDto<bool>.Fail("Feed item not found.");

            // Toggle: আগে save করা থাকলে unsave করব
            var existingSave = await unitOfWork.FeedItemSaves
                .GetByFeedItemAndUserAsync(request.Dto.FeedItemId, request.Dto.UserId, cancellationToken);

            if (existingSave is not null)
            {
                // 🔹 cancellationToken forward করা হলো
                await unitOfWork.FeedItemSaves.RemoveAsync(existingSave, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return ApiResponseDto<bool>.Ok(false, "Feed item unsaved.");
            }

            var save = new FeedItemSave
            {
                FeedItemId = request.Dto.FeedItemId,
                UserId = request.Dto.UserId
            };

            await unitOfWork.FeedItemSaves.AddAsync(save, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponseDto<bool>.Ok(true, "Feed item saved.");
        }
    }
}
