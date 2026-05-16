using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;
using HealthcareHospitalManagement.Application.Queries.Posts;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Posts
{
    public class GetPostInteractionsQueryHandler(IFeedUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetPostInteractionsQuery, ApiResponseDto<PostInteractionSummaryDto>>
    {
        public async Task<ApiResponseDto<PostInteractionSummaryDto>> Handle(
            GetPostInteractionsQuery request,
            CancellationToken cancellationToken)
        {
            var interaction = await unitOfWork.PostInteractions
                .GetByPostIdAsync(request.PostId, cancellationToken);

            if (interaction is null)
                return ApiResponseDto<PostInteractionSummaryDto>.Fail("Post not found.");

            var dto = mapper.Map<PostInteractionSummaryDto>(interaction);

            return ApiResponseDto<PostInteractionSummaryDto>.Ok(dto);
        }
    }
}
