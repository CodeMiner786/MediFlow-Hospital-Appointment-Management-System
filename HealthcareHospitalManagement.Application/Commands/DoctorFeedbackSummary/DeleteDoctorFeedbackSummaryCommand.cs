using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorFeedbackSummary
{
    public record DeleteDoctorFeedbackSummaryCommand(Guid SummaryId)
    : IRequest<ApiResponseDto<bool>>;

}
