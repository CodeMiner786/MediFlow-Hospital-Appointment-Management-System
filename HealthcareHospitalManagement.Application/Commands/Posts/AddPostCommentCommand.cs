using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Posts
{
    public record AddPostCommentCommand(AddPostCommentRequestDto Dto) 
        : IRequest<ApiResponseDto<PostCommentResponseDto>>;

}
