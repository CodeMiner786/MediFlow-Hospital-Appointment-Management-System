using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Patients
{
    public sealed record DeletePatientCommand(Guid PatientId)
    : IRequest<bool>;

}
