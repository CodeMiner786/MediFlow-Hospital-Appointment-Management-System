using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Patients;
using HealthcareHospitalManagement.Application.DTOs.Patient;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Patients
{
    public sealed class UpdatePatientCommandHandler(
        IPatientUnitOfWork uow,
        IMapper mapper)
        : IRequestHandler<UpdatePatientCommand, PatientDetailResponseDto>
    {
        public async Task<PatientDetailResponseDto> Handle(
            UpdatePatientCommand request,
            CancellationToken cancellationToken)
        {
            // ── Patient খোঁজা ──
            var patient = await uow.Patients.GetByIdAsync(request.PatientId, cancellationToken)
                ?? throw new KeyNotFoundException(
                    $"PatientId '{request.PatientId}' দিয়ে কোনো পেশেন্ট পাওয়া যায়নি।");

            // ── Partial Update — null fields skip হবে (MappingProfile এ ForAllMembers আছে) ──
            mapper.Map(request.Dto, patient);

            // ── BMI রিক্যালকুলেট ──
            if (patient.WeightKg.HasValue && patient.HeightCm is > 0)
            {
                var hm = patient.HeightCm.Value / 100m;
                patient.BMI = Math.Round(patient.WeightKg.Value / (hm * hm), 2);
            }

            patient.UpdatedAt = DateTime.UtcNow;

            // ── Update with CancellationToken ──
            await uow.Patients.UpdateAsync(patient, cancellationToken);

            // ── SaveChanges with CancellationToken ──
            await uow.SaveChangesAsync(cancellationToken);

            return mapper.Map<PatientDetailResponseDto>(patient);
        }
    }

}
