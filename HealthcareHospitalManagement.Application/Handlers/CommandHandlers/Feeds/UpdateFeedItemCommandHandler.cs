using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Feeds;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Feed;
using HealthcareHospitalManagement.Domain.Entities.Feed;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Feeds
{
    // 🔹 Primary constructor ব্যবহার করা হলো
    public class UpdateFeedItemCommandHandler(IFeedUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<UpdateFeedItemCommand, ApiResponseDto<FeedItemResponseDto>>
    {
        public async Task<ApiResponseDto<FeedItemResponseDto>> Handle(
            UpdateFeedItemCommand request,
            CancellationToken cancellationToken)
        {
            var feedItem = await unitOfWork.FeedItems.GetByIdAsync(request.Dto.Id, cancellationToken);
            if (feedItem is null)
                return ApiResponseDto<FeedItemResponseDto>.Fail("Feed item not found.");

            // 🔹 Patch update with AutoMapper (null-safe)
            mapper.Map(request.Dto, feedItem);

            // 🔹 Tags আপডেট (simplified collection initialization)
            feedItem.Tags = request.Dto.Tags?
                .Select(tag => new FeedItemTag { Tag = tag, FeedItemId = feedItem.Id })
                .ToList() ?? [];

            unitOfWork.FeedItems.Update(feedItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // 🔹 ResponseDto তৈরি
            var response = mapper.Map<FeedItemResponseDto>(feedItem);
            response.Tags = [.. feedItem.Tags.Select(t => t.Tag)];
            response.LikeCount = feedItem.Likes.Count;
            response.SaveCount = feedItem.Saves.Count;
            response.OwnerName = $"{feedItem.OwnerUser.FirstName} {feedItem.OwnerUser.LastName}";
            response.OwnerImageUrl = feedItem.OwnerUser.ProfileImageUrl;
            response.ItemType = feedItem.ItemType.ToString();
            response.PrimaryAction = feedItem.PrimaryAction.ToString();

            return ApiResponseDto<FeedItemResponseDto>.Ok(response, "Feed item updated successfully.");
        }
    }
}
