using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Feed;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Feeds
{
    public record GetFeedItemsQuery(FeedFilterRequestDto Filter)
        : IRequest<ApiResponseDto<PagedResultDto<FeedItemResponseDto>>>;

}
