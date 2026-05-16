using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Feed;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Feeds
{
    public record CreateFeedItemCommand(CreateFeedItemRequestDto Dto) 
        : IRequest<ApiResponseDto<FeedItemResponseDto>>;

}
