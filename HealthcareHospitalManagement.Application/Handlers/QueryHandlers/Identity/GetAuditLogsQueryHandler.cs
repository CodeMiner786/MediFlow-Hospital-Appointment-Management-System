using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Identity;
using HealthcareHospitalManagement.Application.Queries.Identity;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Identity
{
    public class GetAuditLogsQueryHandler(
        IIdentityUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<GetAuditLogsQuery, ApiResponse<PagedResultDto<AuditLogResponseDto>>>
    {
        private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<PagedResultDto<AuditLogResponseDto>>> Handle(
            GetAuditLogsQuery query,
            CancellationToken cancellationToken)
        {
            var dto = query.RequestDto;

            var pagedResponse = await _unitOfWork.AuditLogs.GetFilteredLogsAsync(
                dto.UserId,
                dto.Action,
                dto.UserRole,
                dto.DateFrom,
                dto.DateTo,
                dto.PageNumber,
                dto.PageSize,
                cancellationToken);

            var result = pagedResponse.ToMappedPagedResult<
                Domain.Entities.Identity.AuditLog,
                AuditLogResponseDto>(_mapper);

            return ApiResponse<PagedResultDto<AuditLogResponseDto>>.SuccessResponse(
                result,
                "Audit logs retrieved successfully.");
        }
    }
}
