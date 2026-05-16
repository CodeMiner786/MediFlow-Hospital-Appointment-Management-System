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
    public class CreateFeedItemCommandHandler(IFeedUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<CreateFeedItemCommand, ApiResponseDto<FeedItemResponseDto>>
    {
        public async Task<ApiResponseDto<FeedItemResponseDto>> Handle(
            CreateFeedItemCommand request,
            CancellationToken cancellationToken)
        {
            // DTO → Entity mapping
            var feedItem = mapper.Map<FeedItem>(request.Dto);

            // 🔹 Tags initialization simplified (C# shorthand)
            feedItem.Tags = request.Dto.Tags?
                .Select(tag => new FeedItemTag { Tag = tag, FeedItemId = feedItem.Id })
                .ToList() ?? [];

            await unitOfWork.FeedItems.AddAsync(feedItem, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // Entity → ResponseDto mapping
            var response = mapper.Map<FeedItemResponseDto>(feedItem);

            // 🔹 Manual calculated fields
            response.Tags = [.. feedItem.Tags.Select(t => t.Tag)];
            response.LikeCount = feedItem.Likes.Count;
            response.SaveCount = feedItem.Saves.Count;
            response.OwnerName = $"{feedItem.OwnerUser.FirstName} {feedItem.OwnerUser.LastName}";
            response.OwnerImageUrl = feedItem.OwnerUser.ProfileImageUrl;
            response.ItemType = feedItem.ItemType.ToString();
            response.PrimaryAction = feedItem.PrimaryAction.ToString();

            return ApiResponseDto<FeedItemResponseDto>.Ok(response, "Feed item created successfully.");
        }
    }
}
