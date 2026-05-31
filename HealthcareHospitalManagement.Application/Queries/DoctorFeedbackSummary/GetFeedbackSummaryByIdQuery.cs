using HealthcareHospitalManagement.Application.DTOs.DoctorFeedbackSummary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorFeedbackSummary
{
    public record GetFeedbackSummaryByIdQuery(Guid SummaryId)
    : IRequest<ApiResponseDto<DoctorFeedbackSummaryDto>>;

}
