using HealthcareHospitalManagement.Application.DTOs.Appointment;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Appointments
{
    public record RescheduleAppointmentCommand(RescheduleAppointmentRequestDto RequestDto)
        : IRequest<ApiResponse<bool>>;
}
