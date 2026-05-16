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
    public sealed record GetMedicineByIdQuery(Guid MedicineId)
    : IRequest<ApiResponseDto<MedicineResponseDto>>;


}
