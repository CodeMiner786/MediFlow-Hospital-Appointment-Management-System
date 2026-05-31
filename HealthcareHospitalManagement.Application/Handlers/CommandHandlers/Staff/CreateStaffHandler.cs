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
    public class CreateStaffHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateStaffCommand, ApiResponseDto<StaffDto>>
    {
        public async Task<ApiResponseDto<StaffDto>> Handle(
            CreateStaffCommand request, CancellationToken ct)
        {
            var exists = await uow.Staff.GetByStaffCodeAsync(request.Dto.StaffCode);
            if (exists is not null)
                return ApiResponseDto<StaffDto>.FailResponse(
                    "A staff member with this code already exists.");

            var entity = mapper.Map<Domain.Entities.Doctor.StaffEntity>(request.Dto);

            await uow.Staff.AddAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<StaffDto>.SuccessResponse(
                mapper.Map<StaffDto>(entity), "Staff created successfully.");
        }
    }

}
