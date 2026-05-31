using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.DoctorEarnings
{
    public record GetDoctorTotalShareQuery(Guid DoctorId, bool OnlyPaid)
    : IRequest<ApiResponseDto<decimal>>;

}
