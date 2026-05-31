using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.DoctorNotes;
using HealthcareHospitalManagement.Application.DTOs.DoctorNote;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorNotes
{
    public class UpdateDoctorNoteHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateDoctorNoteCommand, ApiResponseDto<DoctorNoteDto>>
    {
        public async Task<ApiResponseDto<DoctorNoteDto>> Handle(
            UpdateDoctorNoteCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorNotes.GetByIdAsync(request.NoteId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorNoteDto>.FailResponse("Note not found.");

            mapper.Map(request.Dto, entity);

            await uow.DoctorNotes.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorNoteDto>.SuccessResponse(
                mapper.Map<DoctorNoteDto>(entity), "Note updated successfully.");
        }
    }

}
