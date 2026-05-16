using HealthcareHospitalManagement.Application.DTOs.Appointment;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;

namespace HealthcareHospitalManagement.Application.Commands.Appointments
{
    public record  BookAppointmentCommand(BookAppointmentRequestDto RequestDto)
        : IRequest<ApiResponse<Guid>>;
    
}
