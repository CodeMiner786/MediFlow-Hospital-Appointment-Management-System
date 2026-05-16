using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Pharmacys;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Pharmacys
{
    public sealed class CreatePrescriptionCommandHandler(
    IPharmacyUnitOfWork uow,
    IMapper mapper)
    : IRequestHandler<CreatePrescriptionCommand, ApiResponseDto<PrescriptionResponseDto>>
    {
        public async Task<ApiResponseDto<PrescriptionResponseDto>> Handle(
            CreatePrescriptionCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // ── ① AutoMapper দিয়ে Entity ও Items তৈরি ──
            var prescription = mapper.Map<Prescription>(dto);

            // ── ② Business Logic fields ──
            prescription.PrescriptionCode = $"RX-{Random.Shared.Next(100000, 999999)}";
            prescription.PrescribedDate = DateTime.UtcNow;
            prescription.Status = PrescriptionStatus.Pending;
            prescription.ValidUntil = DateTime.UtcNow.AddDays(30);
            prescription.Items = mapper.Map<ICollection<PrescriptionItem>>(dto.Items);

            await uow.Prescriptions.AddAsync(prescription, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<PrescriptionResponseDto>(prescription);
            return ApiResponseDto<PrescriptionResponseDto>.Ok(result, "Prescription সফলভাবে তৈরি হয়েছে।");
        }
    }

}
