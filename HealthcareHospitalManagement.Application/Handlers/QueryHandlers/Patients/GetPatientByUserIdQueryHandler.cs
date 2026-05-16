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
    public sealed class GetPatientByUserIdQueryHandler(IPatientUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetPatientByUserIdQuery, PatientDetailResponseDto>
    {
        public async Task<PatientDetailResponseDto> Handle(
            GetPatientByUserIdQuery request, CancellationToken ct)
        {
            var patient = await uow.Patients
                .GetByApplicationUserIdAsync(request.ApplicationUserId, ct)
                ?? throw new KeyNotFoundException(
                    $"UserId '{request.ApplicationUserId}' এর জন্য কোনো পেশেন্ট প্রোফাইল নেই।");

            return mapper.Map<PatientDetailResponseDto>(patient);
        }
    }

}
