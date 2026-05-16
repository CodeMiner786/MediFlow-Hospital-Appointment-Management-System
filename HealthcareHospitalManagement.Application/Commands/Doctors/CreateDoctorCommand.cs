using HealthcareHospitalManagement.Application.DTOs.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Commands.Doctors
{
    // ✅ Dto নামে রাখো — consistent থাকবে
    public record CreateDoctorCommand(CreateDoctorRequestDto Dto) : IRequest<Guid>;
}
