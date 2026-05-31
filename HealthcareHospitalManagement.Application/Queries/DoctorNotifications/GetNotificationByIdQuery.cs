using HealthcareHospitalManagement.Application.DTOs.DoctorNotification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorNotifications
{
    public record GetNotificationByIdQuery(Guid NotificationId)
    : IRequest<ApiResponseDto<DoctorNotificationDto>>;

}
