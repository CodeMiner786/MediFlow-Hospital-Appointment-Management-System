using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Posts;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;
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
    public class AddPostCommentCommandHandler : IRequestHandler<AddPostCommentCommand, ApiResponseDto<PostCommentResponseDto>>
    {
        private readonly IFeedUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddPostCommentCommandHandler(IFeedUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponseDto<PostCommentResponseDto>> Handle(
            AddPostCommentCommand request,
            CancellationToken cancellationToken)
        {
            // PostInteraction খুঁজে আনা হচ্ছে — comment PostInteraction এর সাথে linked
            var interaction = await _unitOfWork.PostInteractions
                .GetByPostIdAsync(request.Dto.PostId, cancellationToken);

            if (interaction is null)
                return ApiResponseDto<PostCommentResponseDto>.Fail("Post not found.");

            var comment = new PostComment
            {
                PostInteractionId = interaction.Id,
                UserId = request.Dto.UserId,
                Content = request.Dto.Content,
                ParentCommentId = request.Dto.ParentCommentId
            };

            await _unitOfWork.PostComments.AddAsync(comment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Comment এর author info load করার জন্য reload
            var savedComment = await _unitOfWork.PostComments
                .GetByIdWithUserAsync(comment.Id, cancellationToken);

            var response = _mapper.Map<PostCommentResponseDto>(savedComment);
            response.AuthorName = $"{savedComment!.User.FirstName} {savedComment.User.LastName}";
            response.AuthorImageUrl = savedComment.User.ProfileImageUrl;
            response.ReplyCount = savedComment.Replies.Count;

            return ApiResponseDto<PostCommentResponseDto>.Ok(response, "Comment added successfully.");
        }
    }

}
