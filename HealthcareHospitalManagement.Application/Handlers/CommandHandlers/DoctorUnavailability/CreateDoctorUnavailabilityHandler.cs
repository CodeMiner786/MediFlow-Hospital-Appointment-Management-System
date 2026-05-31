using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.DoctorUnavailability;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorUnavailability;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorUnavailability
{
    public sealed class CreateDoctorUnavailabilityHandler(IDoctorUnitOfWork uow, IMapper mapper)
        : IRequestHandler<CreateDoctorUnavailabilityCommand, ApiResponseDto<DoctorUnavailabilityDto>>
    {
        public async Task<ApiResponseDto<DoctorUnavailabilityDto>> Handle(
            CreateDoctorUnavailabilityCommand request, CancellationToken ct)
        {
            var entity = mapper.Map<
                HealthcareHospitalManagement.Domain.Entities.Doctor.DoctorUnavailability>(request.Dto);

            await uow.DoctorUnavailabilities.AddAsync(entity, ct);
            var saved = await uow.SaveChangesAsync(ct);

            if (saved <= 0)
            {
                // ❌ Fail case: save failed
                return ApiResponseDto<DoctorUnavailabilityDto>.FailResponse(
                    "Failed to create doctor unavailability record.");
            }

            // ✅ Success case
            return ApiResponseDto<DoctorUnavailabilityDto>.SuccessResponse(
                mapper.Map<DoctorUnavailabilityDto>(entity),
                "Unavailability record created successfully.");
        }
    }
}
