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
    public class GetUrgentNotificationsHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetUrgentNotificationsQuery, ApiResponseDto<PagedResultDto<DoctorNotificationDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorNotificationDto>>> Handle(
            GetUrgentNotificationsQuery request, CancellationToken ct)
        {
            var stream = uow.DoctorNotifications.GetUrgentNotificationsStream(request.DoctorId);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorNotification, DoctorNotificationDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorNotificationDto>>.SuccessResponse(paged);
        }
    }


}
