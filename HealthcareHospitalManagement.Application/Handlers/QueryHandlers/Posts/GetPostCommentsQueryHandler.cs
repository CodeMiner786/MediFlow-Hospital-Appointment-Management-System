using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;
using HealthcareHospitalManagement.Application.Queries.Posts;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Posts
{
    public class GetPostCommentsQueryHandler(IFeedUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetPostCommentsQuery, ApiResponseDto<PagedResultDto<PostCommentResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<PostCommentResponseDto>>> Handle(
            GetPostCommentsQuery request,
            CancellationToken cancellationToken)
        {
            var pagedResult = await unitOfWork.PostComments.GetPagedByPostIdAsync(
                postId: request.PostId,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken);

            var mappedItems = pagedResult.Data.Select(comment =>
            {
                var dto = mapper.Map<PostCommentResponseDto>(comment);
                dto.AuthorName = $"{comment.User.FirstName} {comment.User.LastName}";
                dto.AuthorImageUrl = comment.User.ProfileImageUrl;
                dto.ReplyCount = comment.Replies.Count;
                return dto;
            }).ToList();

            var paged = new PagedResultDto<PostCommentResponseDto>(
                mappedItems,
                pagedResult.TotalRecords,
                pagedResult.PageNumber,
                pagedResult.PageSize);

            return ApiResponseDto<PagedResultDto<PostCommentResponseDto>>.Ok(paged);
        }
    }
}
