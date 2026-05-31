using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorNotification;
using HealthcareHospitalManagement.Application.Helpers.Stream;
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
    public class GetUnreadNotificationsHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetUnreadNotificationsQuery, ApiResponseDto<PagedResultDto<DoctorNotificationDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorNotificationDto>>> Handle(
            GetUnreadNotificationsQuery request, CancellationToken ct)
        {
            var stream = uow.DoctorNotifications.GetUnreadNotificationsByDoctorStream(request.DoctorId);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorNotification, DoctorNotificationDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorNotificationDto>>.SuccessResponse(paged);
        }
    }

}
