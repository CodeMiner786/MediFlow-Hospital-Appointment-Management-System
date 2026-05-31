using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Staff;
using HealthcareHospitalManagement.Application.DTOs.Staff;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Staff
{
    public class UpdateStaffHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateStaffCommand, ApiResponseDto<StaffDto>>
    {
        public async Task<ApiResponseDto<StaffDto>> Handle(
            UpdateStaffCommand request, CancellationToken ct)
        {
            var entity = await uow.Staff.GetByIdAsync(request.StaffId, ct);
            if (entity is null)
                return ApiResponseDto<StaffDto>.FailResponse("Staff member not found.");

            mapper.Map(request.Dto, entity);

            await uow.Staff.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<StaffDto>.SuccessResponse(
                mapper.Map<StaffDto>(entity), "Staff updated successfully.");
        }
    }

}
