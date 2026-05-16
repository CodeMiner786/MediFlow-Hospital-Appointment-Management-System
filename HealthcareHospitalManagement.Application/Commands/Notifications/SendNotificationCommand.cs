using HealthcareHospitalManagement.Application.DTOs.Notification;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Notifications
{
    public record SendNotificationCommand(SendNotificationRequestDto RequestDto)
    : IRequest<ApiResponse<UserNotificationResponseDto>>;

}
