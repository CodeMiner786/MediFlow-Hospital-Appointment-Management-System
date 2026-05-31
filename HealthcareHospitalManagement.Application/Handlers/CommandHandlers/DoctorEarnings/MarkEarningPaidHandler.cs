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
    public class MarkEarningPaidHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<MarkEarningPaidCommand, ApiResponseDto<DoctorEarningDto>>
    {
        public async Task<ApiResponseDto<DoctorEarningDto>> Handle(
            MarkEarningPaidCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorEarnings.GetByIdAsync(request.EarningId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorEarningDto>.FailResponse("Earning record not found.");

            if (entity.IsPaid)
                return ApiResponseDto<DoctorEarningDto>.FailResponse("Earning is already marked as paid.");

            entity.IsPaid = true;
            entity.PaidAt = DateTime.UtcNow;
            entity.PaymentReference = request.Dto.PaymentReference;

            await uow.DoctorEarnings.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorEarningDto>.SuccessResponse(
                mapper.Map<DoctorEarningDto>(entity), "Earning marked as paid.");
        }
    }

}
