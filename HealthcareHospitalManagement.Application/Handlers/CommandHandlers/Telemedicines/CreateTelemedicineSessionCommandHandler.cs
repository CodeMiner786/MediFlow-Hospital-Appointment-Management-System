using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Telemedicines;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Telemedicine;
using HealthcareHospitalManagement.Domain.Entities.Telemedicine;
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
    public sealed class CreateTelemedicineSessionCommandHandler(
    ITelemedicineUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<CreateTelemedicineSessionCommand, ApiResponseDto<TelemedicineSessionResponseDto>>
    {
        public async Task<ApiResponseDto<TelemedicineSessionResponseDto>> Handle(
            CreateTelemedicineSessionCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // ── ① AutoMapper দিয়ে Entity তৈরি ──
            var session = mapper.Map<TelemedicineSession>(dto);

            // ── ② Business Logic fields ──
            session.SessionCode = $"TM-{Random.Shared.Next(100000, 999999)}";
            session.Status = TelemedicineSessionStatus.Scheduled;

            // ── ③ Provider অনুযায়ী MeetingLink ও Token জেনারেট ──
            // NOTE: Production এ এখানে Zoom/Daily.co/Jitsi SDK call করতে হবে
            var roomId = Guid.NewGuid().ToString("N")[..12].ToUpper();
            session.RoomId = roomId;
            session.MeetingLink = $"https://meet.healthcare.app/room/{roomId}";
            session.PatientToken = $"pt_{Guid.NewGuid():N}";
            session.DoctorToken = $"dr_{Guid.NewGuid():N}";

            // ── ④ Save ──
            await uow.TelemedicineSessions.AddAsync(session, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<TelemedicineSessionResponseDto>(session);
            return ApiResponseDto<TelemedicineSessionResponseDto>.Ok(
                result, "Telemedicine Session সফলভাবে তৈরি হয়েছে।");
        }
    }

}
