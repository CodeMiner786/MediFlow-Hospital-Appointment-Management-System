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
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, ApiResponseDto<PostResponseDto>>
    {
        private readonly IFeedUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePostCommandHandler(IFeedUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponseDto<PostResponseDto>> Handle(
            UpdatePostCommand request,
            CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.Posts.GetByIdWithDetailsAsync(request.Dto.PostId, cancellationToken);
            if (post is null)
                return ApiResponseDto<PostResponseDto>.Fail("Post not found.");

            // Content güncelle
            if (request.Dto.Content is not null)
                post.Content.TextBody = request.Dto.Content;

            // Audience güncelle
            if (request.Dto.Audience is not null)
                post.Audience.Visibility = request.Dto.Audience.Value;

            // IsPinned güncelle
            if (request.Dto.IsPinned is not null)
                post.Meta.IsPinned = request.Dto.IsPinned.Value;

            // Tags güncelle
            if (request.Dto.Tags is not null)
            {
                post.Tags.Clear();
                foreach (var tag in request.Dto.Tags)
                    post.Tags.Add(new PostTag { Tag = tag, PostId = post.Id });
            }

            // MediaFiles güncelle
            if (request.Dto.MediaUrls is not null)
            {
                post.MediaFiles.Clear();
                foreach (var url in request.Dto.MediaUrls)
                    post.MediaFiles.Add(new PostMedia
                    {
                        MediaUrl = url,
                        MediaType = Domain.Enums.Feed.Posts.PostMediaType.Image,
                        PostId = post.Id
                    });
            }

            _unitOfWork.Posts.Update(post);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<PostResponseDto>(post);

            return ApiResponseDto<PostResponseDto>.Ok(response, "Post updated successfully.");
        }
    }

}
