using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorNotifications
{
    public record DeleteDoctorNotificationCommand(Guid NotificationId)
    : IRequest<ApiResponseDto<bool>>;

}
