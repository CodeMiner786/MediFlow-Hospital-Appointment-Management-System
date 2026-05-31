using HealthcareHospitalManagement.Application.DTOs.DoctorNote;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorNotes
{
    public record GetNoteByAppointmentQuery(Guid AppointmentId)
    : IRequest<ApiResponseDto<DoctorNoteDto>>;

}
