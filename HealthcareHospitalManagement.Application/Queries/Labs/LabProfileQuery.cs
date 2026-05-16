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
    // ✅ Single LabProfile
    public record GetLabProfileQuery(Guid LabProfileId)
        : IRequest<ApiResponse<LabProfileResponseDto>>;

    // ✅ সব Verified Labs
    public record GetVerifiedLabsQuery()
        : IRequest<ApiResponse<List<LabProfileResponseDto>>>;

    // ✅ City অনুযায়ী Labs
    public record GetLabsByCityQuery(string City)
        : IRequest<ApiResponse<List<LabProfileResponseDto>>>;
}
