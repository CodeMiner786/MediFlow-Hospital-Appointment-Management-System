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
    public class GetNoteByAppointmentHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetNoteByAppointmentQuery, ApiResponseDto<DoctorNoteDto>>
    {
        public async Task<ApiResponseDto<DoctorNoteDto>> Handle(
            GetNoteByAppointmentQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorNotes.GetNoteByAppointmentIdAsync(request.AppointmentId);
            if (entity is null)
                return ApiResponseDto<DoctorNoteDto>.FailResponse("No note found for this appointment.");

            return ApiResponseDto<DoctorNoteDto>.SuccessResponse(mapper.Map<DoctorNoteDto>(entity));
        }
    }

}
