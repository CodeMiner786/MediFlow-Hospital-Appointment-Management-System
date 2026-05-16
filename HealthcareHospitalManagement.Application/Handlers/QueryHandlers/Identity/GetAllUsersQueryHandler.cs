using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Auth;
using HealthcareHospitalManagement.Application.Queries.Identity;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Identity
{
    public class GetAllUsersQueryHandler(
        IIdentityUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<GetAllUsersQuery, ApiResponse<PagedResultDto<UserProfileResponseDto>>>
    {
        private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<PagedResultDto<UserProfileResponseDto>>> Handle(
            GetAllUsersQuery query,
            CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.Identity.ApplicationUser> users;

            if (query.Role.HasValue)
            {
                users = await _unitOfWork.Users.GetByRoleAsync(query.Role.Value, cancellationToken);
            }
            else
            {
                var all = new List<Domain.Entities.Identity.ApplicationUser>();
                await foreach (var u in _unitOfWork.Users.GetActiveUsersStream()
                                   .WithCancellation(cancellationToken))
                {
                    all.Add(u);
                }
                users = all;
            }

            var totalCount = users.Count();
            var pagedItems = users
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var result = new PagedResultDto<UserProfileResponseDto>(
                _mapper.Map<List<UserProfileResponseDto>>(pagedItems),
                totalCount,
                query.PageNumber,
                query.PageSize);

            return ApiResponse<PagedResultDto<UserProfileResponseDto>>.SuccessResponse(
                result,
                $"{totalCount} user(s) found.");
        }
    }
}
