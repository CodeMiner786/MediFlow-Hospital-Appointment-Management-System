using HealthcareHospitalManagement.Application.Commands.Doctors;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Doctors
{
    public class RestoreDoctorHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<RestoreDoctorCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            RestoreDoctorCommand request, CancellationToken ct)
        {
            var entity = await uow.Doctors.GetByIdAsync(request.DoctorId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Doctor not found.");

            await uow.Doctors.RestoreAsync(request.DoctorId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Doctor restored successfully.");
        }
    }

}
