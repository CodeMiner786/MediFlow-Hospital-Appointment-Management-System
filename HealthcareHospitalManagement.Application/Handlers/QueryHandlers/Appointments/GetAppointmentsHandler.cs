using AutoMapper;
using HealthcareHospitalManagement.Application.Common; // PagingExtensions
using HealthcareHospitalManagement.Application.DTOs.Appointment;
using HealthcareHospitalManagement.Application.Queries.Appointments;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Appointment;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Appointments
{
    public class GetAppointmentsHandler(IAppointmentUnitOfWork uow, IMapper mapper)
        : IRequestHandler<GetAppointmentsQuery, ApiResponse<PagedResultDto<AppointmentSummaryResponseDto>>>
    {
        public async Task<ApiResponse<PagedResultDto<AppointmentSummaryResponseDto>>> Handle(
            GetAppointmentsQuery request,
            CancellationToken cancellationToken)
        {
            // ✅ Repository থেকে IQueryable পাওয়া যাচ্ছে
            var query = uow.Appointments.GetQueryable();

            // ✅ Pagination + filtering repository এর ভেতরেই হচ্ছে
            var pagedAppointments = await uow.Appointments.ToPagedAsync(
                query,
                request.Filter.PageNumber,
                request.Filter.PageSize,
                cancellationToken
            );

            // ✅ Entity → DTO mapping + pagination metadata
            var pagedResult = pagedAppointments
                .ToMappedPagedResult<AppointmentEntity, AppointmentSummaryResponseDto>(mapper);

            // ✅ Wrap in ApiResponse
            return ApiResponse<PagedResultDto<AppointmentSummaryResponseDto>>
                .SuccessResponse(pagedResult, "Appointments retrieved successfully.");
        }
    }
}
