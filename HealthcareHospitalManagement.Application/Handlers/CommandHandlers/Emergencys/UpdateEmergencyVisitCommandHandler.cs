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
    public sealed class UpdateEmergencyVisitCommandHandler(
    IWardEmergencyUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<UpdateEmergencyVisitCommand, ApiResponseDto<EmergencyVisitResponseDto>>
    {
        public async Task<ApiResponseDto<EmergencyVisitResponseDto>> Handle(
            UpdateEmergencyVisitCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // ── ① Visit খোঁজা ──
            var visit = await uow.EmergencyVisits.GetByIdAsync(dto.EmergencyVisitId, cancellationToken);
            if (visit is null)
                return ApiResponseDto<EmergencyVisitResponseDto>.Fail(
                    $"EmergencyVisitId '{dto.EmergencyVisitId}' পাওয়া যায়নি।");

            // ── ② Partial Update — null না হলেই সেট করা ──
            if (dto.Diagnosis is not null) visit.Diagnosis = dto.Diagnosis;
            if (dto.TreatmentGiven is not null) visit.TreatmentGiven = dto.TreatmentGiven;
            if (dto.Medications is not null) visit.Medications = dto.Medications;
            if (dto.DischargeNotes is not null) visit.DischargeNotes = dto.DischargeNotes;
            if (dto.TransferredTo is not null) visit.TransferredTo = dto.TransferredTo;
            if (dto.DeathCause is not null) visit.DeathCause = dto.DeathCause;

            visit.IsAdmitted = dto.IsAdmitted;
            visit.IsTransferred = dto.IsTransferred;
            visit.IsDeceased = dto.IsDeceased;

            if (dto.AdmissionId.HasValue)
                visit.AdmissionId = dto.AdmissionId;

            // ── ③ Status আপডেট লজিক ──
            if (dto.IsDeceased)
            {
                visit.Status = EmergencyStatus.Deceased;
                visit.TimeOfDeath = DateTime.UtcNow;
                visit.DischargeTime = DateTime.UtcNow;
            }
            else if (dto.IsAdmitted || dto.IsTransferred)
            {
                visit.Status = EmergencyStatus.Discharged;
                visit.DischargeTime = DateTime.UtcNow;
            }
            else
            {
                visit.Status = EmergencyStatus.UnderTreatment;
            }

            visit.UpdatedAt = DateTime.UtcNow;

            // ── ④ Save — GenericRepository.UpdateAsync(entity, ct) ──
            await uow.EmergencyVisits.UpdateAsync(visit, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<EmergencyVisitResponseDto>(visit);
            return ApiResponseDto<EmergencyVisitResponseDto>.Ok(
                result, "Emergency Visit সফলভাবে আপডেট হয়েছে।");
        }
    }

}
