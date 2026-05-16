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
    // ✅ Lab এর সব Tests
    public record GetLabTestsQuery(Guid LabProfileId)
        : IRequest<ApiResponse<List<LabTestResponseDto>>>;

    // ✅ Category অনুযায়ী Tests
    public record GetLabTestsByCategoryQuery(string Category)
        : IRequest<ApiResponse<List<LabTestResponseDto>>>;

    // ✅ Test Name দিয়ে Search
    public record SearchLabTestsQuery(string SearchTerm, Guid LabProfileId)
        : IRequest<ApiResponse<List<LabTestResponseDto>>>;
}
