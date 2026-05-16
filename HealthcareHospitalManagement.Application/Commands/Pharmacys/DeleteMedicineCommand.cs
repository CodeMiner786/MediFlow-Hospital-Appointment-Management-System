using HealthcareHospitalManagement.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Pharmacys
{
    public sealed record DeleteMedicineCommand(Guid MedicineId)
     : IRequest<ApiResponseDto<bool>>;

}
