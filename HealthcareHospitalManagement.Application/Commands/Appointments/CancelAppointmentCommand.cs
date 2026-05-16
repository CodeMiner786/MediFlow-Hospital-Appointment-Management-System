using HealthcareHospitalManagement.Application.DTOs.Appointment;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;

namespace HealthcareHospitalManagement.Application.Commands.Appointments
{
    public record CancelAppointmentCommand(CancelAppointmentRequestDto RequestDto)
        : IRequest<ApiResponse<bool>>;
}
