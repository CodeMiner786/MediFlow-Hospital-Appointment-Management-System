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
    public class DeleteDoctorScheduleHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<DeleteDoctorScheduleCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            DeleteDoctorScheduleCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorSchedules.GetByIdAsync(request.ScheduleId, ct);
            if (entity is null)
                return ApiResponseDto<bool>.FailResponse("Schedule not found.");

            await uow.DoctorSchedules.DeleteAsync(request.ScheduleId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<bool>.SuccessResponse(true, "Schedule deleted successfully.");
        }
    }

}
