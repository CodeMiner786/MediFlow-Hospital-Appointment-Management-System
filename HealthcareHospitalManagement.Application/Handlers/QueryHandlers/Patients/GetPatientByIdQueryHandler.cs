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
    public sealed class GetPatientByIdQueryHandler(IPatientUnitOfWork uow, IMapper mapper)
        : IRequestHandler<GetPatientByIdQuery, PatientSummaryResponseDto>
    {
        public async Task<PatientSummaryResponseDto> Handle(
            GetPatientByIdQuery request, CancellationToken ct)
        {
            var patient = await uow.Patients.GetByIdAsync(request.PatientId, ct)
                ?? throw new KeyNotFoundException(
                    $"PatientId '{request.PatientId}' পাওয়া যায়নি।");

            return mapper.Map<PatientSummaryResponseDto>(patient);
        }
    }
}
