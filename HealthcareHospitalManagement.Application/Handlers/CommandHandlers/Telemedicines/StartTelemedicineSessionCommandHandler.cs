using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Telemedicines;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Telemedicine;
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
    public sealed class StartTelemedicineSessionCommandHandler(
    ITelemedicineUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<StartTelemedicineSessionCommand, ApiResponseDto<TelemedicineSessionResponseDto>>
    {
        public async Task<ApiResponseDto<TelemedicineSessionResponseDto>> Handle(
            StartTelemedicineSessionCommand request,
            CancellationToken cancellationToken)
        {
            var session = await uow.TelemedicineSessions.GetByIdAsync(request.SessionId, cancellationToken);
            if (session is null)
                return ApiResponseDto<TelemedicineSessionResponseDto>.Fail(
                    $"SessionId '{request.SessionId}' পাওয়া যায়নি।");

            // ── ইতিমধ্যে শুরু বা শেষ হয়ে গেছে কিনা চেক ──
            if (session.Status == TelemedicineSessionStatus.InProgress)
                return ApiResponseDto<TelemedicineSessionResponseDto>.Fail(
                    "Session ইতিমধ্যে চলছে।");

            if (session.Status == TelemedicineSessionStatus.Completed ||
                session.Status == TelemedicineSessionStatus.Cancelled)
                return ApiResponseDto<TelemedicineSessionResponseDto>.Fail(
                    $"Session '{session.Status}' — শুরু করা যাবে না।");

            // ── Status → InProgress, StartedAt সেট ──
            session.Status = TelemedicineSessionStatus.InProgress;
            session.StartedAt = DateTime.UtcNow;
            session.UpdatedAt = DateTime.UtcNow;

            await uow.TelemedicineSessions.UpdateAsync(session, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<TelemedicineSessionResponseDto>(session);
            return ApiResponseDto<TelemedicineSessionResponseDto>.Ok(
                result, "Session সফলভাবে শুরু হয়েছে।");
        }
    }

}
