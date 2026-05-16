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
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ApiResponseDto<PostResponseDto>>
    {
        private readonly IFeedUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IFeedUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponseDto<PostResponseDto>> Handle(
            CreatePostCommand request,
            CancellationToken cancellationToken)
        {
            var post = new Post
            {
                AuthorUserId = request.Dto.OwnerUserId,

                // PostContent — owned entity
                Content = new PostContent
                {
                    TextBody = request.Dto.Content,
                    PostType = request.Dto.PostType,
                    Language = request.Dto.Language,
                    FormattedContent = request.Dto.Content
                },

                // PostAudience — owned entity
                Audience = new PostAudience
                {
                    Visibility = request.Dto.Audience,
                    IsAnonymous = request.Dto.IsAnonymous,
                    TargetCity = request.Dto.TargetCity,
                    TargetSpecialty = request.Dto.TargetSpecialty
                },

                // PostMeta — owned entity
                Meta = new PostMeta
                {
                    Status = Domain.Enums.Feed.Posts.PostStatus.Published,
                    PublishedAt = DateTime.UtcNow
                },

                // PostInteraction — boş collection initialize edilsin
                Interactions = new PostInteraction()
            };

            // MediaFiles
            foreach (var url in request.Dto.MediaUrls)
            {
                post.MediaFiles.Add(new PostMedia
                {
                    MediaUrl = url,
                    MediaType = Domain.Enums.Feed.Posts.PostMediaType.Image,
                    PostId = post.Id
                });
            }

            // Tags
            foreach (var tag in request.Dto.Tags)
            {
                post.Tags.Add(new PostTag { Tag = tag, PostId = post.Id });
            }

            await _unitOfWork.Posts.AddAsync(post, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<PostResponseDto>(post);

            return ApiResponseDto<PostResponseDto>.Ok(response, "Post created successfully.");
        }
    }

}
