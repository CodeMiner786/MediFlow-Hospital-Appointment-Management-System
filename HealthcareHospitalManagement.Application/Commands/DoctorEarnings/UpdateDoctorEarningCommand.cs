using HealthcareHospitalManagement.Application.DTOs.DoctorEarning;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorEarnings
{
    public record UpdateDoctorEarningCommand(Guid EarningId, UpdateDoctorEarningDto Dto)
    : IRequest<ApiResponseDto<DoctorEarningDto>>;

}
