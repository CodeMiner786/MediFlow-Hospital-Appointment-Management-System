using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Doctors;
using HealthcareHospitalManagement.Application.Helpers.Stream;
using HealthcareHospitalManagement.Application.Queries.Doctors;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Doctors
{
    public class GetAllDoctorsHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetAllDoctorsQuery, ApiResponseDto<PagedResultDto<DoctorDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorDto>>> Handle(
            GetAllDoctorsQuery request, CancellationToken ct)
        {
            var all = await uow.Doctors.GetAllAsync(ct);

            // ✅ Simplified collection initialization
            var paged = StreamHelper.Paginate<Domain.Entities.Doctor.DoctorEntity, DoctorDto>(
                            [.. all], request.PageNumber, request.PageSize, mapper);

            return ApiResponseDto<PagedResultDto<DoctorDto>>.SuccessResponse(paged);
        }
    }
}
