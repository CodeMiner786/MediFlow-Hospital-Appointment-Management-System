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
    public class SoftDeleteDoctorNoteHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<SoftDeleteDoctorNoteCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            SoftDeleteDoctorNoteCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorNotes.GetByIdAsync(request.NoteId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Note not found.");

            await uow.DoctorNotes.SoftDeleteAsync(request.NoteId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Note soft deleted successfully.");
        }
    }

}
