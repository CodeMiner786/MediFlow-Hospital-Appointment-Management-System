using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Appointment;
using HealthcareHospitalManagement.Application.Queries.Appointments;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Appointments
{
    public class GetAppointmentDetailHandler(IAppointmentUnitOfWork uow, IMapper mapper)
        : IRequestHandler<GetAppointmentDetailQuery, ApiResponse<AppointmentDetailResponseDto>>
    {
        public async Task<ApiResponse<AppointmentDetailResponseDto>> Handle(
            GetAppointmentDetailQuery request,
            CancellationToken cancellationToken)
        {
            var appointment = await uow.Appointments
                .GetByIdAsync(request.AppointmentId, cancellationToken);

            if (appointment is null)
                return ApiResponse<AppointmentDetailResponseDto>.FailResponse(
                    $"Appointment with Id {request.AppointmentId} not found."
                );

            var dto = mapper.Map<AppointmentDetailResponseDto>(appointment);

            return ApiResponse<AppointmentDetailResponseDto>.SuccessResponse(dto, "Appointment retrieved successfully.");
        }
    }

}
