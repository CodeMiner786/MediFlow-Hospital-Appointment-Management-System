using HealthcareHospitalManagement.Application.DTOs.DoctorSchedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.DoctorSchedule
{
    public record CreateDoctorScheduleCommand(CreateDoctorScheduleDto Dto)
    : IRequest<ApiResponseDto<DoctorScheduleDto>>;

}
