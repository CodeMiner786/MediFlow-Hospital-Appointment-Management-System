using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.DoctorLeaves;
using HealthcareHospitalManagement.Application.DTOs.DoctorLeave;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorLeaves
{
    public class UpdateDoctorLeaveHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateDoctorLeaveCommand, ApiResponseDto<DoctorLeaveDto>>
    {
        public async Task<ApiResponseDto<DoctorLeaveDto>> Handle(
            UpdateDoctorLeaveCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorLeaves.GetByIdAsync(request.LeaveId, ct);
            if (entity is null)
                return ApiResponseDto<DoctorLeaveDto>.FailResponse("Leave record not found.");

            if (entity.IsApproved)
                return ApiResponseDto<DoctorLeaveDto>.FailResponse("Approved leave cannot be modified.");

            mapper.Map(request.Dto, entity);

            await uow.DoctorLeaves.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorLeaveDto>.SuccessResponse(
                mapper.Map<DoctorLeaveDto>(entity), "Leave updated successfully.");
        }
    }

}
