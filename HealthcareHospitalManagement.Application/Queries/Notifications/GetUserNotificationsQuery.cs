using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Notification;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Notifications
{
    public record GetUserNotificationsQuery(NotificationFilterRequestDto RequestDto)
    : IRequest<ApiResponse<PagedResultDto<UserNotificationResponseDto>>>;

}
