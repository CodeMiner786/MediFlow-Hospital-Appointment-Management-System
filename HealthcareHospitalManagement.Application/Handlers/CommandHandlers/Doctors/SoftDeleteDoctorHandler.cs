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
    public class SoftDeleteDoctorHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<SoftDeleteDoctorCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            SoftDeleteDoctorCommand request, CancellationToken ct)
        {
            var entity = await uow.Doctors.GetByIdAsync(request.DoctorId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Doctor not found.");

            await uow.Doctors.SoftDeleteAsync(request.DoctorId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Doctor soft deleted successfully.");
        }
    }

}
