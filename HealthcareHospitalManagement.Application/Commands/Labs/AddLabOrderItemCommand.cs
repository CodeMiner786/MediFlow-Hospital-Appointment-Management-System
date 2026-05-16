using HealthcareHospitalManagement.Application.DTOs.Lab;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Labs
{
    public record AddLabOrderItemCommand(Guid LabOrderId, LabOrderItemRequestDto RequestDto)
        : IRequest<ApiResponse<Guid>>;
}
