using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorDocument;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorDocuments
{
    public record GetPagedDocumentsQuery(int PageNumber, int PageSize)
    : IRequest<ApiResponseDto<PagedResultDto<DoctorDocumentDto>>>;

}
