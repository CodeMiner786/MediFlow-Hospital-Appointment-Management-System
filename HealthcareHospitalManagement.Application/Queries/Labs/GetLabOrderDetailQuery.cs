using HealthcareHospitalManagement.Application.DTOs.Lab;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Labs
{
    // ✅ Single Order detail
    public record GetLabOrderDetailQuery(Guid LabOrderId)
        : IRequest<ApiResponse<LabOrderResponseDto>>;

    // ✅ Patient এর সব Orders
    public record GetPatientLabOrdersQuery(Guid PatientId)
        : IRequest<ApiResponse<List<LabOrderResponseDto>>>;
}
