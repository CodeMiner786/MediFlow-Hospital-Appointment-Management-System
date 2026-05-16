using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Wards;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Wards;
using HealthcareHospitalManagement.Domain.Enums.WardBed;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Wards
{
    public sealed class DischargePatientCommandHandler(
    IWardEmergencyUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<DischargePatientCommand, ApiResponseDto<AdmissionResponseDto>>
    {
        public async Task<ApiResponseDto<AdmissionResponseDto>> Handle(
            DischargePatientCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // ── ① Admission খোঁজা ──
            var admission = await uow.Admissions.GetByIdAsync(dto.AdmissionId, cancellationToken);
            if (admission is null)
                return ApiResponseDto<AdmissionResponseDto>.Fail(
                    $"AdmissionId '{dto.AdmissionId}' পাওয়া যায়নি।");

            if (admission.Status == AdmissionStatus.Discharged)
                return ApiResponseDto<AdmissionResponseDto>.Fail(
                    "এই পেশেন্ট আগেই Discharge হয়েছেন।");

            // ── ② Repository এর DischargePatientAsync method ব্যবহার ──
            await uow.Admissions.DischargePatientAsync(
                admissionId: dto.AdmissionId,
                notes: dto.DischargeNotes ?? string.Empty,
                summary: dto.DischargeSummary ?? string.Empty,
                ct: cancellationToken);

            // ── ③ Bed → Available করা ──
            await uow.Beds.UpdateBedStatusAsync(admission.BedId, BedStatus.Available, cancellationToken);

            // ── ④ Ward এর AvailableBeds Count আপডেট ──
            var bed = await uow.Beds.GetByIdAsync(admission.BedId, cancellationToken);
            if (bed is not null)
                await uow.Wards.UpdateBedCountsAsync(bed.WardId, cancellationToken);

            await uow.SaveChangesAsync(cancellationToken);

            // ── ⑤ Updated admission রিটার্ন ──
            var updated = await uow.Admissions.GetByIdAsync(dto.AdmissionId, cancellationToken);
            var result = mapper.Map<AdmissionResponseDto>(updated);
            return ApiResponseDto<AdmissionResponseDto>.Ok(result, "পেশেন্ট সফলভাবে Discharge করা হয়েছে।");
        }
    }

}
