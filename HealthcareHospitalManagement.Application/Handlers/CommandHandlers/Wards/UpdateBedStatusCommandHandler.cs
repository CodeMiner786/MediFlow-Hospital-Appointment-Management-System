using HealthcareHospitalManagement.Application.Commands.Wards;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Wards
{
    public sealed class UpdateBedStatusCommandHandler(IWardEmergencyUnitOfWork uow)
    : IRequestHandler<UpdateBedStatusCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            UpdateBedStatusCommand request,
            CancellationToken cancellationToken)
        {
            // ── Bed আছে কিনা চেক ──
            var bed = await uow.Beds.GetByIdAsync(request.BedId, cancellationToken);
            if (bed is null)
                return ApiResponseDto<bool>.Fail(
                    $"BedId '{request.BedId}' পাওয়া যায়নি।");

            await uow.Beds.UpdateBedStatusAsync(request.BedId, request.NewStatus, cancellationToken);

            // ── Ward এর Count আপডেট ──
            await uow.Wards.UpdateBedCountsAsync(bed.WardId, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return ApiResponseDto<bool>.Ok(true, $"Bed Status সফলভাবে '{request.NewStatus}' করা হয়েছে।");
        }
    }

}
