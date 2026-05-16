using HealthcareHospitalManagement.Application.DTOs.Doctor;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Doctors
{
    public record UpdateDoctorCommand(Guid DoctorId, UpdateDoctorRequestDto Dto)
        : IRequest<ApiResponse<UpdateDoctorRequestDto>>;
}
