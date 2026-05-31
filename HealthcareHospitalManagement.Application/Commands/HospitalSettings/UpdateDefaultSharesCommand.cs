using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.HospitalSettings
{
    public record UpdateDefaultSharesCommand(decimal PlatformShare, decimal DoctorShare)
    : IRequest<ApiResponseDto<bool>>;

}
