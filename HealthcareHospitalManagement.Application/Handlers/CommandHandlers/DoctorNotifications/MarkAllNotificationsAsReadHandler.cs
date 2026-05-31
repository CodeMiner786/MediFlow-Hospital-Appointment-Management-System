using HealthcareHospitalManagement.Application.Commands.DoctorNotifications;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorNotifications
{
    public class MarkAllNotificationsAsReadHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<MarkAllNotificationsAsReadCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            MarkAllNotificationsAsReadCommand request, CancellationToken ct)
        {
            await uow.DoctorNotifications.MarkAllAsReadAsync(request.DoctorId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "All notifications marked as read.");
        }
    }

}
