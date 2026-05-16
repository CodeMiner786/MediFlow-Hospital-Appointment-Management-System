using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Patients;
using HealthcareHospitalManagement.Application.DTOs.Patient;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Patients
{
    public sealed class AddPatientVitalCommandHandler(
        IPatientUnitOfWork uow,
        IMapper mapper)
        : IRequestHandler<AddPatientVitalCommand, PatientVitalResponseDto>
    {
        public async Task<PatientVitalResponseDto> Handle(
            AddPatientVitalCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // ── Patient Validate ──
            var patient = await uow.Patients.GetByIdAsync(dto.PatientId, cancellationToken)
                ?? throw new KeyNotFoundException(
                    $"PatientId '{dto.PatientId}' পাওয়া যায়নি। Vital রেকর্ড করা যাচ্ছে না।");

            // ── AutoMapper দিয়ে Entity তৈরি ──
            var vital = mapper.Map<PatientVital>(dto);

            // ── Business Logic fields ──
            vital.RecordedAt = DateTime.UtcNow;
            vital.RecordedByName = "System"; // TODO: JWT Claim থেকে লগইন ইউজারের নাম নাও

            // BMI ক্যালকুলেশন — dto তে না থাকলে patient এর existing value ব্যবহার
            var weight = dto.WeightKg ?? patient.WeightKg;
            var height = dto.HeightCm ?? patient.HeightCm;

            if (weight.HasValue && height is > 0)
            {
                var hm = height.Value / 100m;
                vital.BMI = Math.Round(weight.Value / (hm * hm), 2);
            }

            // ── Save ──
            await uow.PatientVitals.AddAsync(vital, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return mapper.Map<PatientVitalResponseDto>(vital);
        }
    }
}
