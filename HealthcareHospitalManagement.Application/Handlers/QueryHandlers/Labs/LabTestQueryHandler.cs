using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Lab;
using HealthcareHospitalManagement.Application.Queries.Labs;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Labs
{
    // ── Lab এর সব Tests ───────────────────────────────────────────────
    public class GetLabTestsHandler(ILabUnitOfWork uow, IMapper mapper)
        : IRequestHandler<GetLabTestsQuery, ApiResponse<List<LabTestResponseDto>>>
    {
        public async Task<ApiResponse<List<LabTestResponseDto>>> Handle(
            GetLabTestsQuery request, CancellationToken cancellationToken)
        {
            var tests = await uow.LabTests.GetTestsByLabProfileIdAsync(
                request.LabProfileId);
            var result = mapper.Map<List<LabTestResponseDto>>(tests);

            return ApiResponse<List<LabTestResponseDto>>.SuccessResponse(
                result, "Lab tests retrieved successfully.");
        }
    }

    // ── Category অনুযায়ী Tests ───────────────────────────────────────
    public class GetLabTestsByCategoryHandler(ILabUnitOfWork uow, IMapper mapper)
        : IRequestHandler<GetLabTestsByCategoryQuery, ApiResponse<List<LabTestResponseDto>>>
    {
        public async Task<ApiResponse<List<LabTestResponseDto>>> Handle(
            GetLabTestsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var tests = await uow.LabTests.GetTestsByCategoryAsync(request.Category);
            var result = mapper.Map<List<LabTestResponseDto>>(tests);

            return ApiResponse<List<LabTestResponseDto>>.SuccessResponse(
                result, "Lab tests retrieved successfully.");
        }
    }

    // ── Test Name দিয়ে Search ─────────────────────────────────────────
    public class SearchLabTestsHandler(ILabUnitOfWork uow, IMapper mapper)
        : IRequestHandler<SearchLabTestsQuery, ApiResponse<List<LabTestResponseDto>>>
    {
        public async Task<ApiResponse<List<LabTestResponseDto>>> Handle(
            SearchLabTestsQuery request, CancellationToken cancellationToken)
        {
            var tests = await uow.LabTests.SearchTestsByNameAsync(
                request.SearchTerm, request.LabProfileId);
            var result = mapper.Map<List<LabTestResponseDto>>(tests);

            return ApiResponse<List<LabTestResponseDto>>.SuccessResponse(
                result, "Lab tests search results retrieved successfully.");
        }
    }
}
