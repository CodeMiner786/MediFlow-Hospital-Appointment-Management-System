using HealthcareHospitalManagement.Application.DTOs.DoctorNote;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorNotes
{
    public record GetNoteByIdQuery(Guid NoteId)
    : IRequest<ApiResponseDto<DoctorNoteDto>>;

}
