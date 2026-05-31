using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.DoctorEarning;
using HealthcareHospitalManagement.Application.Queries.DoctorEarnings;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.DoctorEarnings
{
    public class GetEarningByAppointmentHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetEarningByAppointmentQuery, ApiResponseDto<DoctorEarningDto>>
    {
        public async Task<ApiResponseDto<DoctorEarningDto>> Handle(
            GetEarningByAppointmentQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorEarnings.GetByAppointmentIdAsync(request.AppointmentId);
            if (entity is null)
                return ApiResponseDto<DoctorEarningDto>.FailResponse("No earning found for this appointment.");

            return ApiResponseDto<DoctorEarningDto>.SuccessResponse(
                mapper.Map<DoctorEarningDto>(entity));
        }
    }

}
