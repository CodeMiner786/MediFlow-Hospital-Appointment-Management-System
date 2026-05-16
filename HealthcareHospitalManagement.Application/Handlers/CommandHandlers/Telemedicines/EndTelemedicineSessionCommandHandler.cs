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
    public sealed class EndTelemedicineSessionCommandHandler(
    ITelemedicineUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<EndTelemedicineSessionCommand, ApiResponseDto<TelemedicineSessionResponseDto>>
    {
        public async Task<ApiResponseDto<TelemedicineSessionResponseDto>> Handle(
            EndTelemedicineSessionCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var session = await uow.TelemedicineSessions.GetByIdAsync(dto.SessionId, cancellationToken);

            if (session is null)
                return ApiResponseDto<TelemedicineSessionResponseDto>.Fail(
                    $"SessionId '{dto.SessionId}' পাওয়া যায়নি।");

            if (session.Status != TelemedicineSessionStatus.InProgress)
                return ApiResponseDto<TelemedicineSessionResponseDto>.Fail(
                    "শুধুমাত্র InProgress Session শেষ করা যাবে।");

            // ── Clinical notes ──
            if (dto.DoctorNotes is not null) session.DoctorNotes = dto.DoctorNotes;
            if (dto.Prescription is not null) session.Prescription = dto.Prescription;
            if (dto.FollowUpInstructions is not null) session.FollowUpInstructions = dto.FollowUpInstructions;
            if (dto.FollowUpDate is not null) session.FollowUpDate = dto.FollowUpDate;

            // ── Duration হিসাব ──
            session.EndedAt = DateTime.UtcNow;
            session.Status = TelemedicineSessionStatus.Completed;
            session.UpdatedAt = DateTime.UtcNow;

            if (session.StartedAt.HasValue)
                session.DurationMinutes = (int)(session.EndedAt.Value - session.StartedAt.Value).TotalMinutes;

            await uow.TelemedicineSessions.UpdateAsync(session, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<TelemedicineSessionResponseDto>(session);
            return ApiResponseDto<TelemedicineSessionResponseDto>.Ok(
                result, "Session সফলভাবে শেষ হয়েছে।");
        }
    }

}
