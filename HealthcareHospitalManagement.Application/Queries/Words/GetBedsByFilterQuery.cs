using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Wards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Words
{
    public sealed record GetBedsByFilterQuery(BedFilterRequestDto Dto)
    : IRequest<ApiResponseDto<PagedResultDto<BedResponseDto>>>;

}
