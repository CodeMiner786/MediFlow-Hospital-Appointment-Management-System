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
    public class GetDoctorFullProfileHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetDoctorFullProfileQuery, ApiResponseDto<DoctorDto>>
    {
        public async Task<ApiResponseDto<DoctorDto>> Handle(
            GetDoctorFullProfileQuery request, CancellationToken ct)
        {
            var entity = await uow.Doctors.GetDoctorFullProfileAsync(request.DoctorId);
            if (entity is null)
                return ApiResponseDto<DoctorDto>.FailResponse("Doctor not found.");

            return ApiResponseDto<DoctorDto>.SuccessResponse(mapper.Map<DoctorDto>(entity));
        }
    }

}
