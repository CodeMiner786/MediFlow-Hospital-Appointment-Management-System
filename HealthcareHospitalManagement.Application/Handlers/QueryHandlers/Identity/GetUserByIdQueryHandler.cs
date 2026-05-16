using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Auth;
using HealthcareHospitalManagement.Application.Queries.Identity;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Identity
{
    public class GetUserByIdQueryHandler(
        IIdentityUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<GetUserByIdQuery, ApiResponse<UserProfileResponseDto>>
    {
        private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<UserProfileResponseDto>> Handle(
            GetUserByIdQuery query,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(query.UserId, cancellationToken);

            if (user is null)
                return ApiResponse<UserProfileResponseDto>.FailResponse("User not found.");

            var dto = _mapper.Map<UserProfileResponseDto>(user);

            return ApiResponse<UserProfileResponseDto>.SuccessResponse(dto, "User retrieved successfully.");
        }
    }
}
