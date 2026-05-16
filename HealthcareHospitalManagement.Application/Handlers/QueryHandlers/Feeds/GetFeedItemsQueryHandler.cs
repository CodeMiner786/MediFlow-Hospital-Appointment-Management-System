using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Feed;
using HealthcareHospitalManagement.Application.Queries.Feeds;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Domain.Entities.Feed;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Feeds
{
    // 🔹 Primary constructor ব্যবহার করা হলো
    public class GetFeedItemsQueryHandler(IFeedUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetFeedItemsQuery, ApiResponseDto<PagedResultDto<FeedItemResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<FeedItemResponseDto>>> Handle(
            GetFeedItemsQuery request,
            CancellationToken cancellationToken)
        {
            // ✅ Repository থেকে IQueryable আনো
            var query = unitOfWork.FeedItems.GetQueryable();

            // ✅ Enum দিয়ে filter করো
            if (request.Filter.ItemType.HasValue)
                query = query.Where(f => f.ItemType == request.Filter.ItemType.Value);

            if (!string.IsNullOrEmpty(request.Filter.City))
                query = query.Where(f => f.City == request.Filter.City);

            if (!string.IsNullOrEmpty(request.Filter.Tag))
                query = query.Where(f => f.Tags.Any(t => t.Tag == request.Filter.Tag));

            // ✅ Total count বের করো
            var totalCount = await query.CountAsync(cancellationToken);

            // ✅ Pagination apply করো
            var data = await query
                .Include(f => f.Tags)
                .Include(f => f.Likes)
                .Include(f => f.Saves)
                .Include(f => f.OwnerUser)
                .Skip((request.Filter.PageNumber - 1) * request.Filter.PageSize)
                .Take(request.Filter.PageSize)
                .ToListAsync(cancellationToken);

            // ✅ Wrapper বানাও
            var pagedResponse = PagedResponse<FeedItem>.Create(
                data,
                request.Filter.PageNumber,
                request.Filter.PageSize,
                totalCount
            );

            // ✅ DTO তে map করো
            var pagedResult = pagedResponse.ToMappedPagedResult<FeedItem, FeedItemResponseDto>(mapper);

            // ✅ Extra enrich fields (simplified collection init)
            foreach (var dto in pagedResult.Items)
            {
                var entity = data.First(f => f.Id == dto.Id);
                dto.Tags = [.. entity.Tags.Select(t => t.Tag)];
                dto.LikeCount = entity.Likes.Count;
                dto.SaveCount = entity.Saves.Count;
                dto.OwnerName = $"{entity.OwnerUser.FirstName} {entity.OwnerUser.LastName}";
                dto.OwnerImageUrl = entity.OwnerUser.ProfileImageUrl;
                dto.ItemType = entity.ItemType.ToString();
                dto.PrimaryAction = entity.PrimaryAction.ToString();
            }

            // ✅ ApiResponse wrap করো
            return ApiResponseDto<PagedResultDto<FeedItemResponseDto>>.Ok(pagedResult);
        }
    }
}
