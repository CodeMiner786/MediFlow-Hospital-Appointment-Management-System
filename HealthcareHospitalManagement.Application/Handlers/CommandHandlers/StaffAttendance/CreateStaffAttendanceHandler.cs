using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.StaffAttendance;
using HealthcareHospitalManagement.Application.DTOs.Staff.StaffAttendance;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.StaffAttendance
{
    public class CreateStaffAttendanceHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateStaffAttendanceCommand, ApiResponseDto<StaffAttendanceDto>>
    {
        public async Task<ApiResponseDto<StaffAttendanceDto>> Handle(
            CreateStaffAttendanceCommand request, CancellationToken ct)
        {
            // একই তারিখে ইতোমধ্যে অ্যাটেনডেন্স আছে কিনা চেক
            var existing = await uow.StaffAttendances.GetTodayAttendanceAsync(request.Dto.StaffId, ct);
            if (existing is not null && existing.AttendanceDate == request.Dto.AttendanceDate)
                return ApiResponseDto<StaffAttendanceDto>.FailResponse(
                    "Attendance already recorded for this staff on the given date.");

            var entity = mapper.Map<Domain.Entities.Doctor.StaffAttendance>(request.Dto);

            await uow.StaffAttendances.AddAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<StaffAttendanceDto>.SuccessResponse(
                mapper.Map<StaffAttendanceDto>(entity), "Attendance created successfully.");
        }
    }

}
