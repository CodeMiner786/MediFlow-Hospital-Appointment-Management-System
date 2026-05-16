using HealthcareHospitalManagement.Application.Commands.Posts;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Posts
{
    public class SharePostCommandHandler : IRequestHandler<SharePostCommand, ApiResponseDto<bool>>
    {
        private readonly IFeedUnitOfWork _unitOfWork;

        public SharePostCommandHandler(IFeedUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDto<bool>> Handle(
            SharePostCommand request,
            CancellationToken cancellationToken)
        {
            var interaction = await _unitOfWork.PostInteractions
                .GetByPostIdAsync(request.Dto.PostId, cancellationToken);

            if (interaction is null)
                return ApiResponseDto<bool>.Fail("Post not found.");

            var share = new PostShare
            {
                PostInteractionId = interaction.Id,
                UserId = request.Dto.UserId,
                ShareNote = request.Dto.ShareNote,
                SharedAt = DateTime.UtcNow
            };

            await _unitOfWork.PostShares.AddAsync(share, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ApiResponseDto<bool>.Ok(true, "Post shared successfully.");
        }
    }

}
