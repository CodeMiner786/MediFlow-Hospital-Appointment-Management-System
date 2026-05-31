using HealthcareHospitalManagement.Application.DTOs.DoctorFeedbackSummary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorFeedbackSummary
{
    public record CreateDoctorFeedbackSummaryCommand(CreateDoctorFeedbackSummaryDto Dto)
    : IRequest<ApiResponseDto<DoctorFeedbackSummaryDto>>;

}
