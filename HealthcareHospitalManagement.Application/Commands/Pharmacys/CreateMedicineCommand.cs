using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Pharmacys
{
    public sealed record CreateMedicineCommand(CreateMedicineRequestDto Dto)
      : IRequest<ApiResponseDto<MedicineResponseDto>>;


}
