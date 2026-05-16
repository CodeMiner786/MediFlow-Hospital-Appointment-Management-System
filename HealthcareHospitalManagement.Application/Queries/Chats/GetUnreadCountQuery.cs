using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Chats
{
    public record GetUnreadCountQuery(Guid UserId)
    : IRequest<ApiResponse<int>>;

}
