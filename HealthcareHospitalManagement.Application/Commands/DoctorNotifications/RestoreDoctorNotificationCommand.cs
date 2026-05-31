using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorNotifications
{
    public record RestoreDoctorNotificationCommand(Guid NotificationId)
    : IRequest<ApiResponseDto<bool>>;

}
