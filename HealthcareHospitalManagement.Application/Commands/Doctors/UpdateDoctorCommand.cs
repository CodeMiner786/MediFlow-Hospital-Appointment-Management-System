using HealthcareHospitalManagement.Application.DTOs.Doctors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Doctors
{
    public record UpdateDoctorCommand(Guid DoctorId, UpdateDoctorDto Dto)
    : IRequest<ApiResponseDto<DoctorDto>>;

}
