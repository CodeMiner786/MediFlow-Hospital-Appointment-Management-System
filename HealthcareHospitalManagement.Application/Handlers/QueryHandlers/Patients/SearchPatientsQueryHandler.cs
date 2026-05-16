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
    public sealed class SearchPatientsQueryHandler(IPatientUnitOfWork uow, IMapper mapper)
    : IRequestHandler<SearchPatientsQuery, IEnumerable<PatientSummaryResponseDto>>
    {
        public async Task<IEnumerable<PatientSummaryResponseDto>> Handle(
            SearchPatientsQuery request, CancellationToken ct)
        {
            var dto = request.Dto;

            var result = dto.BloodGroup.HasValue
                ? await uow.Patients.GetPatientsByBloodGroupAsync(dto.BloodGroup.Value, ct)
                : await uow.Patients.SearchPatientsAsync(
                    dto.FirstName ?? dto.LastName ?? dto.PhoneNumber ?? dto.PatientCode ?? "", ct);

            var paged = result
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize);

            return mapper.Map<IEnumerable<PatientSummaryResponseDto>>(paged);
        }
    }

}
