using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.DoctorEarnings;
using HealthcareHospitalManagement.Application.DTOs.DoctorEarning;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorEarnings
{
    public class CreateDoctorEarningHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateDoctorEarningCommand, ApiResponseDto<DoctorEarningDto>>
    {
        public async Task<ApiResponseDto<DoctorEarningDto>> Handle(
            CreateDoctorEarningCommand request, CancellationToken ct)
        {
            var entity = mapper.Map<Domain.Entities.Doctor.DoctorEarning>(request.Dto);

            // হাসপাতাল ও ডাক্তারের শেয়ার অটো-ক্যালকুলেট
            entity.HospitalShareAmount = entity.TotalFee * (entity.HospitalSharePercent / 100m);
            entity.DoctorShareAmount = entity.TotalFee - entity.HospitalShareAmount;

            await uow.DoctorEarnings.AddAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            var dto = mapper.Map<DoctorEarningDto>(entity);
            return ApiResponseDto<DoctorEarningDto>.SuccessResponse(dto, "Doctor earning created successfully.");
        }
    }

}
