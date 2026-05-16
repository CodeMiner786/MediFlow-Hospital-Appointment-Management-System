using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;
using HealthcareHospitalManagement.Application.Queries.Posts;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Posts
{
    public class GetPostByIdQueryHandler(IFeedUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetPostByIdQuery, ApiResponseDto<PostResponseDto>>
    {
        public async Task<ApiResponseDto<PostResponseDto>> Handle(
            GetPostByIdQuery request,
            CancellationToken cancellationToken)
        {
            var post = await unitOfWork.Posts.GetByIdWithDetailsAsync(request.Id, cancellationToken);
            if (post is null)
                return ApiResponseDto<PostResponseDto>.Fail("Post not found.");

            var dto = mapper.Map<PostResponseDto>(post);

            return ApiResponseDto<PostResponseDto>.Ok(dto);
        }
    }
}
