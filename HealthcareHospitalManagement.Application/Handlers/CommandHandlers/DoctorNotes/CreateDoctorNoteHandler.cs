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
    public class CreateDoctorNoteHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateDoctorNoteCommand, ApiResponseDto<DoctorNoteDto>>
    {
        public async Task<ApiResponseDto<DoctorNoteDto>> Handle(
            CreateDoctorNoteCommand request, CancellationToken ct)
        {
            var entity = mapper.Map<Domain.Entities.Doctor.DoctorNote>(request.Dto);

            await uow.DoctorNotes.AddAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorNoteDto>.SuccessResponse(
                mapper.Map<DoctorNoteDto>(entity), "Note created successfully.");
        }
    }


}
