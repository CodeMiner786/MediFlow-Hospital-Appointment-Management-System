using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Feed;
using HealthcareHospitalManagement.Application.Queries.Feeds;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Feeds
{
    // 🔹 Primary constructor ব্যবহার করা হলো
    public class GetFeedItemByIdQueryHandler(IFeedUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetFeedItemByIdQuery, ApiResponseDto<FeedItemResponseDto>>
    {
        public async Task<ApiResponseDto<FeedItemResponseDto>> Handle(
            GetFeedItemByIdQuery request,
            CancellationToken cancellationToken)
        {
            // 🔹 Repository থেকে feed item আনো
            var feedItem = await unitOfWork.FeedItems.GetFeedWithDetailsAsync(request.Id);
            if (feedItem is null)
                return ApiResponseDto<FeedItemResponseDto>.Fail("Feed item not found.");

            // 🔹 AutoMapper দিয়ে DTO বানাও
            var dto = mapper.Map<FeedItemResponseDto>(feedItem);

            // 🔹 Collection initialization simplified
            dto.Tags = [.. feedItem.Tags.Select(t => t.Tag)];
            dto.LikeCount = feedItem.Likes.Count;
            dto.SaveCount = feedItem.Saves.Count;
            dto.OwnerName = $"{feedItem.OwnerUser.FirstName} {feedItem.OwnerUser.LastName}";
            dto.OwnerImageUrl = feedItem.OwnerUser.ProfileImageUrl;
            dto.ItemType = feedItem.ItemType.ToString();
            dto.PrimaryAction = feedItem.PrimaryAction.ToString();

            // 🔹 ApiResponse wrap করো
            return ApiResponseDto<FeedItemResponseDto>.Ok(dto);
        }
    }
}
