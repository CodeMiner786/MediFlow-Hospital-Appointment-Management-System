using HealthcareHospitalManagement.Application.DTOs.Doctors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Doctors
{
    public record GetTopRatedDoctorsQuery(int Count = 10)
    : IRequest<ApiResponseDto<IEnumerable<DoctorDto>>>;

}
