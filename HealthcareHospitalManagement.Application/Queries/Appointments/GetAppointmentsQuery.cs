using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Appointment;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Appointments
{
    public record GetAppointmentsQuery (AppointmentFilterRequestDto Filter) 
        : IRequest<ApiResponse<PagedResultDto<AppointmentSummaryResponseDto>>>;
    
}
