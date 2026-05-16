using HealthcareHospitalManagement.Application.DTOs.Identity;
using HealthcareHospitalManagement.Application.Queries.Identity;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Identity
{
    public class GetRoleAssignmentLogsQueryHandler(
        IIdentityUnitOfWork unitOfWork
    ) : IRequestHandler<GetRoleAssignmentLogsQuery, ApiResponse<List<RoleAssignmentLogResponseDto>>>
    {
        private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ApiResponse<List<RoleAssignmentLogResponseDto>>> Handle(
            GetRoleAssignmentLogsQuery query,
            CancellationToken cancellationToken)
        {
            IEnumerable<RoleAssignmentLog> logs;

            if (query.TargetUserId.HasValue)
            {
                var list = new List<RoleAssignmentLog>();
                await foreach (var log in _unitOfWork.RoleAssignmentLogs
                                   .GetLogsByTargetUserStream(query.TargetUserId.Value)
                                   .WithCancellation(cancellationToken))
                {
                    list.Add(log);
                }
                logs = list;
            }
            else
            {
                logs = await _unitOfWork.RoleAssignmentLogs.GetLogsByDateRangeAsync(
                    DateTime.UtcNow.AddDays(-30),
                    DateTime.UtcNow);
            }

            var result = logs.Select(log => new RoleAssignmentLogResponseDto
            {
                Id = log.Id,
                UserName = $"{log.User?.FirstName} {log.User?.LastName}".Trim(),
                OldRole = log.FromRole.ToString(),
                NewRole = log.ToRole.ToString(),
                AssignedByName = $"{log.AssignedByUser?.FirstName} {log.AssignedByUser?.LastName}".Trim(),
                Reason = log.Reason ?? string.Empty,
                AssignedAt = log.AssignedAt
            }).ToList();

            return ApiResponse<List<RoleAssignmentLogResponseDto>>.SuccessResponse(
                result,
                $"{result.Count} role assignment log(s) found.");
        }
    }
}
