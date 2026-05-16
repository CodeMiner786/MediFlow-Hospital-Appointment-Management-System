using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Pharmacys;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Pharmacys
{
    public sealed class CreateMedicineCommandHandler(
    IPharmacyUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<CreateMedicineCommand, ApiResponseDto<MedicineResponseDto>>
    {
        public async Task<ApiResponseDto<MedicineResponseDto>> Handle(
            CreateMedicineCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // ── ① Pharmacy আছে কিনা চেক ──
            var pharmacy = await uow.PharmacyProfiles.GetByIdAsync(dto.PharmacyProfileId, cancellationToken);
            if (pharmacy is null)
                return ApiResponseDto<MedicineResponseDto>.Fail(
                    $"PharmacyProfileId '{dto.PharmacyProfileId}' পাওয়া যায়নি।");

            // ── ② AutoMapper দিয়ে Entity তৈরি ──
            var medicine = mapper.Map<Medicine>(dto);

            // ── ③ Business Logic — AutoMapper সেট করতে পারবে না ──
            medicine.MedicineCode = $"MED-{Random.Shared.Next(10000, 99999)}";

            await uow.Medicines.AddAsync(medicine, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<MedicineResponseDto>(medicine);
            return ApiResponseDto<MedicineResponseDto>.Ok(result, "Medicine সফলভাবে তৈরি হয়েছে।");
        }
    }

}
