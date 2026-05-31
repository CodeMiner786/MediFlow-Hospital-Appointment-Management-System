using HealthcareHospitalManagement.Application.DTOs.DoctorDocument;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorDocuments
{
    public record GetDocumentByIdQuery(Guid DocumentId)
    : IRequest<ApiResponseDto<DoctorDocumentDto>>;

}
