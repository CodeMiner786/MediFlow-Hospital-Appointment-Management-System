using HealthcareHospitalManagement.Application.DTOs.Doctors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Doctors
{
    public record GetDoctorByIdQuery(Guid DoctorId)
    : IRequest<ApiResponseDto<DoctorDto>>;

}
