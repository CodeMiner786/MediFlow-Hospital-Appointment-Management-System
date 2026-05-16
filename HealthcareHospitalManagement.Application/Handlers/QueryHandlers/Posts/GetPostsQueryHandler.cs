using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;
using HealthcareHospitalManagement.Application.Queries.Posts;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Posts
{
    public class GetPostsQueryHandler(IFeedUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetPostsQuery, ApiResponseDto<PagedResultDto<PostResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<PostResponseDto>>> Handle(
            GetPostsQuery request,
            CancellationToken cancellationToken)
        {
            var pagedResult = await unitOfWork.Posts.GetPagedAsync(
                postType: request.Filter.PostType,
                visibility: request.Filter.Visibility,
                status: request.Filter.Status,
                tag: request.Filter.Tag,
                keyword: request.Filter.Keyword,
                authorId: request.Filter.AuthorId,
                pageNumber: request.Filter.PageNumber,
                pageSize: request.Filter.PageSize,
                cancellationToken: cancellationToken);

            var mappedItems = pagedResult.Data.Select(post =>
            {
                var dto = mapper.Map<PostResponseDto>(post);
                return dto;
            }).ToList();

            var paged = new PagedResultDto<PostResponseDto>(
                mappedItems,
                pagedResult.TotalRecords,
                pagedResult.PageNumber,
                pagedResult.PageSize);

            return ApiResponseDto<PagedResultDto<PostResponseDto>>.Ok(paged);
        }
    }
}
