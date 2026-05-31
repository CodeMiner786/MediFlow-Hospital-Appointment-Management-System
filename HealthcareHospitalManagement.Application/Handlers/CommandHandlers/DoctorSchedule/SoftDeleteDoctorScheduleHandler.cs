using HealthcareHospitalManagement.Application.Commands.DoctorSchedule;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorSchedule
{
    public class SoftDeleteDoctorScheduleHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<SoftDeleteDoctorScheduleCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            SoftDeleteDoctorScheduleCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorSchedules.GetByIdAsync(request.ScheduleId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Schedule not found.");

            await uow.DoctorSchedules.SoftDeleteAsync(request.ScheduleId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Schedule soft deleted successfully.");
        }
    }

}
