using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorNotes
{
    public record SoftDeleteDoctorNoteCommand(Guid NoteId)
    : IRequest<ApiResponseDto<bool>>;

}
