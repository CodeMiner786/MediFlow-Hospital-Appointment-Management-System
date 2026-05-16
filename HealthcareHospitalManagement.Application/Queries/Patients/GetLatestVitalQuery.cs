using HealthcareHospitalManagement.Application.DTOs.Patient;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Patients
{
    public sealed record GetLatestVitalQuery(Guid PatientId)
    : IRequest<PatientVitalResponseDto?>;

}
