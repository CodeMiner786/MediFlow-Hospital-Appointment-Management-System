using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Patient;
using HealthcareHospitalManagement.Application.Queries.Patients;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Patients
{
    public sealed class GetPatientFullProfileQueryHandler(IPatientUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetPatientFullProfileQuery, PatientDetailResponseDto>
    {
        public async Task<PatientDetailResponseDto> Handle(
            GetPatientFullProfileQuery request, CancellationToken ct)
        {
            // GetFullProfileByIdAsync → Include(Vitals, MedicalRecords, Admissions, HealthLogs)
            var patient = await uow.Patients.GetFullProfileByIdAsync(request.PatientId, ct)
                ?? throw new KeyNotFoundException(
                    $"PatientId '{request.PatientId}' পাওয়া যায়নি।");

            return mapper.Map<PatientDetailResponseDto>(patient);
        }
    }

}
