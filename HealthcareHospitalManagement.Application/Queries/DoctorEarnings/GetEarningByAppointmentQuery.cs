using HealthcareHospitalManagement.Application.DTOs.DoctorEarning;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorEarnings
{
    public record GetEarningByAppointmentQuery(Guid AppointmentId)
    : IRequest<ApiResponseDto<DoctorEarningDto>>;

}
