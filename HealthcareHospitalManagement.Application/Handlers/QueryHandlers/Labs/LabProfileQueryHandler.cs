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
    // ── Single LabProfile ─────────────────────────────────────────────
    public class GetLabProfileHandler(ILabUnitOfWork uow, IMapper mapper)
        : IRequestHandler<GetLabProfileQuery, ApiResponse<LabProfileResponseDto>>
    {
        public async Task<ApiResponse<LabProfileResponseDto>> Handle(
            GetLabProfileQuery request, CancellationToken cancellationToken)
        {
            // LabProfile আছে কিনা check
            var labProfile = await uow.LabProfiles.GetByIdAsync(
                request.LabProfileId, cancellationToken)
                    ?? throw new KeyNotFoundException(
                        $"Lab profile not found: {request.LabProfileId}");

            var result = mapper.Map<LabProfileResponseDto>(labProfile);

            return ApiResponse<LabProfileResponseDto>.SuccessResponse(
                result, "Lab profile retrieved successfully.");
        }
    }

    // ── সব Verified Labs ──────────────────────────────────────────────
    public class GetVerifiedLabsHandler(ILabUnitOfWork uow, IMapper mapper)
        : IRequestHandler<GetVerifiedLabsQuery, ApiResponse<List<LabProfileResponseDto>>>
    {
        public async Task<ApiResponse<List<LabProfileResponseDto>>> Handle(
            GetVerifiedLabsQuery request, CancellationToken cancellationToken)
        {
            var labs = await uow.LabProfiles.GetVerifiedLabsAsync(cancellationToken);
            var result = mapper.Map<List<LabProfileResponseDto>>(labs);

            return ApiResponse<List<LabProfileResponseDto>>.SuccessResponse(
                result, "Verified labs retrieved successfully.");
        }
    }

    // ── City অনুযায়ী Labs ─────────────────────────────────────────────
    public class GetLabsByCityHandler(ILabUnitOfWork uow, IMapper mapper)
        : IRequestHandler<GetLabsByCityQuery, ApiResponse<List<LabProfileResponseDto>>>
    {
        public async Task<ApiResponse<List<LabProfileResponseDto>>> Handle(
            GetLabsByCityQuery request, CancellationToken cancellationToken)
        {
            var labs = await uow.LabProfiles.GetActiveLabsByCityAsync(
                request.City, cancellationToken);
            var result = mapper.Map<List<LabProfileResponseDto>>(labs);

            return ApiResponse<List<LabProfileResponseDto>>.SuccessResponse(
                result, "Labs retrieved successfully.");
        }
    }
}
