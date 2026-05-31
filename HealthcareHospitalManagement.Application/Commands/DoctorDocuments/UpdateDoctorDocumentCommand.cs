using HealthcareHospitalManagement.Application.DTOs.DoctorDocument;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorDocuments
{
    public record UpdateDoctorDocumentCommand(Guid DocumentId, UpdateDoctorDocumentDto Dto)
    : IRequest<ApiResponseDto<DoctorDocumentDto>>;

}
