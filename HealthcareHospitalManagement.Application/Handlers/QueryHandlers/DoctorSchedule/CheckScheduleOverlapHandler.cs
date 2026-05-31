using HealthcareHospitalManagement.Application.Queries.DoctorSchedule;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.DoctorSchedule
{
    public class CheckScheduleOverlapHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<CheckScheduleOverlapQuery, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            CheckScheduleOverlapQuery request, CancellationToken ct)
        {
            var hasOverlap = await uow.DoctorSchedules.HasScheduleOverlapAsync(
                request.DoctorId, request.Day, request.Start, request.End);

            return ApiResponseDto<bool>.SuccessResponse(hasOverlap,
                hasOverlap ? "Schedule overlap detected." : "No overlap found.");
        }
    }

}
