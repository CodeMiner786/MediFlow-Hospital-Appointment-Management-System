using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Doctor;
using HealthcareHospitalManagement.Application.Queries.Doctors;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Doctors
{
    public class GetDoctorDetailHandler(IDoctorUnitOfWork uow, IMapper mapper)
       : IRequestHandler<GetDoctorDetailQuery, DoctorDetailResponseDto>
    {
        public async Task<DoctorDetailResponseDto> Handle(GetDoctorDetailQuery request, CancellationToken cancellationToken)
        {
            var doctorEntity = await uow.Doctors.GetQueryable()
                .FirstOrDefaultAsync(d => d.Id == request.DoctorId, cancellationToken);

            if (doctorEntity == null)
                throw new KeyNotFoundException("Doctor not found");

            // ✅ AutoMapper দিয়ে entity → DTO map করা হচ্ছে
            return mapper.Map<DoctorDetailResponseDto>(doctorEntity);
        }
    }
}
