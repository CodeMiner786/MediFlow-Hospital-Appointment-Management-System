using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Doctors;
using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Doctors
{
    public record GetDoctorsBySpecializationQuery(DoctorSpecialization Specialization, int PageNumber = 1, int PageSize = 10)
    : IRequest<ApiResponseDto<PagedResultDto<DoctorDto>>>;

}
