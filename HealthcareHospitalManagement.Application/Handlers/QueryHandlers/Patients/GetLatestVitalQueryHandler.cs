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
    public sealed class GetLatestVitalQueryHandler(IPatientUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetLatestVitalQuery, PatientVitalResponseDto?>
    {
        public async Task<PatientVitalResponseDto?> Handle(
            GetLatestVitalQuery request, CancellationToken ct)
        {
            var vital = await uow.PatientVitals.GetLatestVitalsByPatientIdAsync(request.PatientId);

            return vital is null ? null : mapper.Map<PatientVitalResponseDto>(vital);
        }
    }

}
