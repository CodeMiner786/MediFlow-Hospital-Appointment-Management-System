using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Patients;
using HealthcareHospitalManagement.Application.DTOs.Patient;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.Patient;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Patients
{
    public sealed class CreatePatientCommandHandler(
       IPatientUnitOfWork uow,
       IMapper mapper)
       : IRequestHandler<CreatePatientCommand, PatientSummaryResponseDto>
    {
        public async Task<PatientSummaryResponseDto> Handle(
            CreatePatientCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // ── Duplicate চেক ──
            var existingByNid = await uow.Patients.GetByNationalIdAsync(dto.NationalId, cancellationToken);
            if (existingByNid is not null)
                throw new InvalidOperationException(
                    $"এই NationalId '{dto.NationalId}' দিয়ে পেশেন্ট আগেই রেজিস্টার্ড আছে।");

            var existingByUser = await uow.Patients
                .GetByApplicationUserIdAsync(dto.ApplicationUserId, cancellationToken);
            if (existingByUser is not null)
                throw new InvalidOperationException(
                    "এই User এর জন্য পেশেন্ট প্রোফাইল আগেই তৈরি আছে।");

            // ── AutoMapper দিয়ে Entity তৈরি ──
            var patient = mapper.Map<Patient>(dto);

            // ── Business Logic fields — AutoMapper সেট করতে পারবে না ──
            patient.PatientCode = $"P-{Random.Shared.Next(100000, 999999)}";
            patient.PatientType = PatientType.General;

            var today = DateTime.UtcNow;
            var age = today.Year - dto.DateOfBirth.Year;
            if (dto.DateOfBirth.Date > today.AddYears(-age)) age--;
            patient.Age = age;

            // ── Save ──
            await uow.Patients.AddAsync(patient, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return mapper.Map<PatientSummaryResponseDto>(patient);
        }
    }
}
