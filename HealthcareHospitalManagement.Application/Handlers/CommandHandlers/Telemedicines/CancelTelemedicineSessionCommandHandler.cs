using HealthcareHospitalManagement.Application.Commands.Telemedicines;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Domain.Enums.Telemedicine;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Telemedicines
{
    public sealed class CancelTelemedicineSessionCommandHandler(ITelemedicineUnitOfWork uow)
    : IRequestHandler<CancelTelemedicineSessionCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            CancelTelemedicineSessionCommand request,
            CancellationToken cancellationToken)
        {
            var session = await uow.TelemedicineSessions.GetByIdAsync(request.SessionId, cancellationToken);
            if (session is null)
                return ApiResponseDto<bool>.Fail(
                    $"SessionId '{request.SessionId}' পাওয়া যায়নি।");

            if (session.Status == TelemedicineSessionStatus.Completed)
                return ApiResponseDto<bool>.Fail("Completed Session Cancel করা যাবে না।");

            if (session.Status == TelemedicineSessionStatus.Cancelled)
                return ApiResponseDto<bool>.Fail("Session ইতিমধ্যেই Cancelled আছে।");

            session.Status = TelemedicineSessionStatus.Cancelled;
            session.UpdatedAt = DateTime.UtcNow;

            await uow.TelemedicineSessions.UpdateAsync(session, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return ApiResponseDto<bool>.Ok(true, "Session সফলভাবে Cancel করা হয়েছে।");
        }
    }

}
