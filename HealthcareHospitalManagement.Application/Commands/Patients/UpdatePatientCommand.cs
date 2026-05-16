using HealthcareHospitalManagement.Application.DTOs.Patient;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Patients
{
    public sealed record UpdatePatientCommand(Guid PatientId, UpdatePatientRequestDto Dto)
    : IRequest<PatientDetailResponseDto>;

}
