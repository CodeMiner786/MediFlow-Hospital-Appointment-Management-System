using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Identity;
using HealthcareHospitalManagement.Application.Queries.Identity;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Identity
{
    public class GetUserLoginHistoryQueryHandler(
        IIdentityUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<GetUserLoginHistoryQuery, ApiResponse<List<UserLoginHistoryResponseDto>>>
    {
        private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<List<UserLoginHistoryResponseDto>>> Handle(
            GetUserLoginHistoryQuery query,
            CancellationToken cancellationToken)
        {
            var histories = await _unitOfWork.UserLoginHistories
                .GetRecentSuccessLoginsAsync(query.UserId, query.RecentCount);

            var result = _mapper.Map<List<UserLoginHistoryResponseDto>>(histories);

            return ApiResponse<List<UserLoginHistoryResponseDto>>.SuccessResponse(
                result,
                $"{result.Count} login history record(s) found.");
        }
    }
}
