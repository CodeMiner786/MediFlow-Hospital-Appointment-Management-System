using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.DoctorNote;
using HealthcareHospitalManagement.Application.Queries.DoctorNotes;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.DoctorNotes
{
    public class GetNoteByIdHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetNoteByIdQuery, ApiResponseDto<DoctorNoteDto>>
    {
        public async Task<ApiResponseDto<DoctorNoteDto>> Handle(
            GetNoteByIdQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorNotes.GetByIdAsync(request.NoteId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorNoteDto>.FailResponse("Note not found.");

            return ApiResponseDto<DoctorNoteDto>.SuccessResponse(mapper.Map<DoctorNoteDto>(entity));
        }
    }

}
