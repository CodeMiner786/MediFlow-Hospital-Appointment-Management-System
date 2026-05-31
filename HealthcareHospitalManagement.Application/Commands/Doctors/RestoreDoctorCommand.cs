using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Doctors
{
    public record RestoreDoctorCommand(Guid DoctorId)
    : IRequest<ApiResponseDto<bool>>;

}
