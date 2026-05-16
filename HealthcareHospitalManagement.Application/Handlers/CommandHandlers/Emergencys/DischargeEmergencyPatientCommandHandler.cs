using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Emergencys;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Emergency;
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
    public sealed class DischargeEmergencyPatientCommandHandler(
    IWardEmergencyUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<DischargeEmergencyPatientCommand, ApiResponseDto<EmergencyVisitResponseDto>>
    {
        public async Task<ApiResponseDto<EmergencyVisitResponseDto>> Handle(
            DischargeEmergencyPatientCommand request,
            CancellationToken cancellationToken)
        {
            // ── ① Visit খোঁজা ──
            var visit = await uow.EmergencyVisits.GetByIdAsync(
                request.EmergencyVisitId, cancellationToken);

            if (visit is null)
                return ApiResponseDto<EmergencyVisitResponseDto>.Fail(
                    $"EmergencyVisitId '{request.EmergencyVisitId}' পাওয়া যায়নি।");

            // ── ② আগেই Discharge হয়ে গেছে কিনা চেক ──
            if (visit.Status == EmergencyStatus.Discharged || visit.Status == EmergencyStatus.Deceased)
                return ApiResponseDto<EmergencyVisitResponseDto>.Fail(
                    $"এই Visit ইতিমধ্যে '{visit.Status}' হয়েছে।");

            // ── ③ Discharge ──
            visit.Status = EmergencyStatus.Discharged;
            visit.DischargeTime = DateTime.UtcNow;
            visit.UpdatedAt = DateTime.UtcNow;

            await uow.EmergencyVisits.UpdateAsync(visit, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<EmergencyVisitResponseDto>(visit);
            return ApiResponseDto<EmergencyVisitResponseDto>.Ok(
                result, "Emergency পেশেন্ট সফলভাবে Discharge করা হয়েছে।");
        }
    }

}
