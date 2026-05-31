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
    public class UpdateDoctorEarningHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateDoctorEarningCommand, ApiResponseDto<DoctorEarningDto>>
    {
        public async Task<ApiResponseDto<DoctorEarningDto>> Handle(
            UpdateDoctorEarningCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorEarnings.GetByIdAsync(request.EarningId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorEarningDto>.FailResponse("Earning record not found.");

            mapper.Map(request.Dto, entity);

            // পুনরায় শেয়ার ক্যালকুলেট
            entity.HospitalShareAmount = entity.TotalFee * (entity.HospitalSharePercent / 100m);
            entity.DoctorShareAmount = entity.TotalFee - entity.HospitalShareAmount;

            await uow.DoctorEarnings.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorEarningDto>.SuccessResponse(
                mapper.Map<DoctorEarningDto>(entity), "Earning updated successfully.");
        }
    }

}
