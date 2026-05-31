using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Doctors;
using HealthcareHospitalManagement.Application.Queries.Doctors;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Doctors
{
    public class GetTopRatedDoctorsHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetTopRatedDoctorsQuery, ApiResponseDto<IEnumerable<DoctorDto>>>
    {
        public async Task<ApiResponseDto<IEnumerable<DoctorDto>>> Handle(
            GetTopRatedDoctorsQuery request, CancellationToken ct)
        {
            var entities = await uow.Doctors.GetTopRatedDoctorsAsync(request.Count);
            return ApiResponseDto<IEnumerable<DoctorDto>>.SuccessResponse(
                mapper.Map<IEnumerable<DoctorDto>>(entities));
        }
    }

}
