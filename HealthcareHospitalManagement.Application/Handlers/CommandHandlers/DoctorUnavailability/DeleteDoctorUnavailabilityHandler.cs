using HealthcareHospitalManagement.Application.Commands.DoctorUnavailability;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorUnavailability
{
    public class DeleteDoctorUnavailabilityHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<DeleteDoctorUnavailabilityCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            DeleteDoctorUnavailabilityCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorUnavailabilities.GetByIdAsync(request.UnavailabilityId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Unavailability record not found.");

            await uow.DoctorUnavailabilities.DeleteAsync(request.UnavailabilityId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Unavailability deleted successfully.");
        }
    }

}
