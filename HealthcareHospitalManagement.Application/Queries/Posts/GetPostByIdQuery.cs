using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Posts
{
    public record GetPostByIdQuery(Guid Id)
        : IRequest<ApiResponseDto<PostResponseDto>>;

}
