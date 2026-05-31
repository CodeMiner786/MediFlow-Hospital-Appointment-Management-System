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
    public class GetDoctorByIdHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetDoctorByIdQuery, ApiResponseDto<DoctorDto>>
    {
        public async Task<ApiResponseDto<DoctorDto>> Handle(
            GetDoctorByIdQuery request, CancellationToken ct)
        {
            var entity = await uow.Doctors.GetByIdAsync(request.DoctorId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorDto>.FailResponse("Doctor not found.");

            return ApiResponseDto<DoctorDto>.SuccessResponse(mapper.Map<DoctorDto>(entity));
        }
    }

}
