using HealthcareHospitalManagement.Application.DTOs.Appointment;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;

namespace HealthcareHospitalManagement.Application.Queries.Appointments
{
    public record GetAppointmentDetailQuery(Guid AppointmentId)
       : IRequest<ApiResponse<AppointmentDetailResponseDto>>;
}
