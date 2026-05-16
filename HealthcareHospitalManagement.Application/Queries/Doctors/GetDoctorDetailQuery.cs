using HealthcareHospitalManagement.Application.DTOs.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Doctors
{
    public record GetDoctorDetailQuery(Guid DoctorId) : IRequest<DoctorDetailResponseDto>;
}
