using HealthcareHospitalManagement.Application.DTOs.Identity;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Identity
{
    public record VerifyOtpCommand(VerifyOtpRequestDto RequestDto)
    : IRequest<ApiResponse<bool>>;

}
