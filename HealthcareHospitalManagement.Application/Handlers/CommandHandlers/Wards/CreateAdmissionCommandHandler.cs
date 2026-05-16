using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Wards;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Wards;
using HealthcareHospitalManagement.Domain.Entities.Wards;
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
    public sealed class CreateAdmissionCommandHandler(
    IWardEmergencyUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<CreateAdmissionCommand, ApiResponseDto<AdmissionResponseDto>>
    {
        public async Task<ApiResponseDto<AdmissionResponseDto>> Handle(
            CreateAdmissionCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // ── ① Bed আছে কিনা এবং Available কিনা চেক ──
            var bed = await uow.Beds.GetByIdAsync(dto.BedId, cancellationToken);
            if (bed is null)
                return ApiResponseDto<AdmissionResponseDto>.Fail(
                    $"BedId '{dto.BedId}' পাওয়া যায়নি।");

            if (bed.Status != BedStatus.Available)
                return ApiResponseDto<AdmissionResponseDto>.Fail(
                    $"এই Bed টি এখন Available নেই। বর্তমান Status: {bed.Status}");

            // ── ② AutoMapper দিয়ে Entity তৈরি ──
            var admission = mapper.Map<Admission>(dto);

            // ── ③ Business Logic fields ──
            admission.AdmissionCode = $"ADM-{Random.Shared.Next(100000, 999999)}";
            admission.Status = AdmissionStatus.Admitted;

            // ── ④ Bed Status → Occupied আপডেট করা ──
            await uow.Beds.UpdateBedStatusAsync(dto.BedId, BedStatus.Occupied, cancellationToken);

            // ── ⑤ Ward এর AvailableBeds Count আপডেট ──
            await uow.Wards.UpdateBedCountsAsync(bed.WardId, cancellationToken);

            await uow.Admissions.AddAsync(admission, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<AdmissionResponseDto>(admission);
            return ApiResponseDto<AdmissionResponseDto>.Ok(result, "পেশেন্ট সফলভাবে Admit করা হয়েছে।");
        }
    }

}
