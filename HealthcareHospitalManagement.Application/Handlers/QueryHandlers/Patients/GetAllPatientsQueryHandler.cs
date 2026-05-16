using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Patient;
using HealthcareHospitalManagement.Application.Queries.Patients;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Patients
{
    public sealed class GetAllPatientsQueryHandler(IPatientUnitOfWork uow, IMapper mapper)
        : IRequestHandler<GetAllPatientsQuery, IEnumerable<PatientSummaryResponseDto>>
    {
        public async Task<IEnumerable<PatientSummaryResponseDto>> Handle(
            GetAllPatientsQuery request, CancellationToken ct)
        {
            // 🔹 এখন CancellationToken pass করা হচ্ছে
            var patients = await uow.Patients.GetAllAsync(ct);

            var paged = patients
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);

            return mapper.Map<IEnumerable<PatientSummaryResponseDto>>(paged);
        }
    }
}
