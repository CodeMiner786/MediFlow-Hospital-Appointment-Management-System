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
    public class GetDoctorByCodeHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetDoctorByCodeQuery, ApiResponseDto<DoctorDto>>
    {
        public async Task<ApiResponseDto<DoctorDto>> Handle(
            GetDoctorByCodeQuery request, CancellationToken ct)
        {
            var entity = await uow.Doctors.GetByDoctorCodeAsync(request.DoctorCode);
            if (entity is null)
                return ApiResponseDto<DoctorDto>.FailResponse("Doctor not found.");

            return ApiResponseDto<DoctorDto>.SuccessResponse(mapper.Map<DoctorDto>(entity));
        }
    }

}
