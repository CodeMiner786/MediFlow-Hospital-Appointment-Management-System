using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Pharmacys
{
    public sealed record GetPharmacyProfileQuery(Guid PharmacyId)
     : IRequest<ApiResponseDto<PharmacyProfileResponseDto>>;


}
