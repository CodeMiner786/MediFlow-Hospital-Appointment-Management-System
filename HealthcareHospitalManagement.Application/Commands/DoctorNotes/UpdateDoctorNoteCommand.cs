using HealthcareHospitalManagement.Application.DTOs.DoctorNote;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorNotes
{
    public record UpdateDoctorNoteCommand(Guid NoteId, UpdateDoctorNoteDto Dto)
    : IRequest<ApiResponseDto<DoctorNoteDto>>;


}
