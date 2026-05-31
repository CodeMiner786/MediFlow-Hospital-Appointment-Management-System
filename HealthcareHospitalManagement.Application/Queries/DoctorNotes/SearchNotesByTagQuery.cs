using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorNote;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorNotes
{
    public record SearchNotesByTagQuery(Guid PatientId, string Tag, int PageNumber = 1, int PageSize = 10)
    : IRequest<ApiResponseDto<PagedResultDto<DoctorNoteDto>>>;

}
