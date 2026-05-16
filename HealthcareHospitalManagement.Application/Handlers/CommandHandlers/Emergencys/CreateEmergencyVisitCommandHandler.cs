using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Emergencys;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Emergency;
using HealthcareHospitalManagement.Domain.Entities.Emergency;
using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Emergencys
{
    public sealed class CreateEmergencyVisitCommandHandler(
    IWardEmergencyUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<CreateEmergencyVisitCommand, ApiResponseDto<EmergencyVisitResponseDto>>
    {
        public async Task<ApiResponseDto<EmergencyVisitResponseDto>> Handle(
            CreateEmergencyVisitCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // ── ① AutoMapper দিয়ে Entity তৈরি ──
            var visit = mapper.Map<EmergencyVisit>(dto);

            // ── ② Business Logic fields ──
            visit.EmergencyCode = $"ER-{Random.Shared.Next(100000, 999999)}";
            visit.Status = EmergencyStatus.Arrived;
            visit.ArrivalTime = dto.ArrivalTime == default ? DateTime.UtcNow : dto.ArrivalTime;
            visit.TriageTime = DateTime.UtcNow;

            // ── ③ Save — GenericRepository.AddAsync(entity, ct) ──
            await uow.EmergencyVisits.AddAsync(visit, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<EmergencyVisitResponseDto>(visit);
            return ApiResponseDto<EmergencyVisitResponseDto>.Ok(
                result, "Emergency Visit সফলভাবে তৈরি হয়েছে।");
        }
    }

}
