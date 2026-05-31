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
    public class CreateDoctorLeaveHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateDoctorLeaveCommand, ApiResponseDto<DoctorLeaveDto>>
    {
        public async Task<ApiResponseDto<DoctorLeaveDto>> Handle(
            CreateDoctorLeaveCommand request, CancellationToken ct)
        {
            var entity = mapper.Map<Domain.Entities.Doctor.DoctorLeave>(request.Dto);

            await uow.DoctorLeaves.AddAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorLeaveDto>.SuccessResponse(
                mapper.Map<DoctorLeaveDto>(entity), "Leave request created successfully.");
        }
    }

}
