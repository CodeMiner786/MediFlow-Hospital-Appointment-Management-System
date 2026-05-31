using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorNote;
using HealthcareHospitalManagement.Application.Helpers.Stream;
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
    public class GetNotesByDoctorHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetNotesByDoctorQuery, ApiResponseDto<PagedResultDto<DoctorNoteDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorNoteDto>>> Handle(
            GetNotesByDoctorQuery request, CancellationToken ct)
        {
            var stream = uow.DoctorNotes.GetNotesByDoctorStream(request.DoctorId);
            var all = await StreamHelper.ToList(stream, ct);
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorNote, DoctorNoteDto>(
                             all, request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorNoteDto>>.SuccessResponse(paged);
        }
    }

}
