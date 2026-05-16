using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Doctors
{
    public record GetDoctorsQuery(DoctorSearchRequestDto Dto)
       : IRequest<PagedResultDto<DoctorSummaryResponseDto>>;
}
