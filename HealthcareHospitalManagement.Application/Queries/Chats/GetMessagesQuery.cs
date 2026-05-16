using HealthcareHospitalManagement.Application.DTOs.Chat;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Chats
{
    public record GetMessagesQuery(MessageFilterRequestDto RequestDto, Guid CurrentUserId)
    : IRequest<ApiResponse<PagedMessagesResponseDto>>;

}
