using HealthcareHospitalManagement.Application.Commands.DoctorNotes;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorNotes
{
    public class DeleteDoctorNoteHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<DeleteDoctorNoteCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            DeleteDoctorNoteCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorNotes.GetByIdAsync(request.NoteId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Note not found.");

            await uow.DoctorNotes.DeleteAsync(request.NoteId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Note deleted successfully.");
        }
    }

}
