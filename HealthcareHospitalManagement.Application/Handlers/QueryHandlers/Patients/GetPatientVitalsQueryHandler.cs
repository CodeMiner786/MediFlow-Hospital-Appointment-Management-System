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
    public sealed class GetPatientVitalsQueryHandler(IPatientUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetPatientVitalsQuery, IEnumerable<PatientVitalResponseDto>>
    {
        public async Task<IEnumerable<PatientVitalResponseDto>> Handle(
            GetPatientVitalsQuery request, CancellationToken ct)
        {
            var vitals = await uow.PatientVitals.GetVitalsByPatientIdAsync(request.PatientId);

            return mapper.Map<IEnumerable<PatientVitalResponseDto>>(vitals);
        }
    }

}
