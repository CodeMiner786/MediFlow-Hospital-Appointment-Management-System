using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.DoctorNotification;
using HealthcareHospitalManagement.Application.Queries.DoctorNotifications;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.DoctorNotifications
{
    public class GetNotificationByIdHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetNotificationByIdQuery, ApiResponseDto<DoctorNotificationDto>>
    {
        public async Task<ApiResponseDto<DoctorNotificationDto>> Handle(
            GetNotificationByIdQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorNotifications.GetByIdAsync(request.NotificationId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorNotificationDto>.FailResponse("Notification not found.");

            return ApiResponseDto<DoctorNotificationDto>.SuccessResponse(
                mapper.Map<DoctorNotificationDto>(entity));
        }
    }

}
