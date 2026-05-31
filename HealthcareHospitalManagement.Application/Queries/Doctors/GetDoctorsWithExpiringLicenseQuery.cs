using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Doctors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Doctors
{
    public record GetDoctorsWithExpiringLicenseQuery(DateTime ThresholdDate, int PageNumber = 1, int PageSize = 10)
    : IRequest<ApiResponseDto<PagedResultDto<DoctorDto>>>;

}
