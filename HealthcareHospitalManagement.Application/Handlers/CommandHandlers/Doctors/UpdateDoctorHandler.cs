using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Doctors;
using HealthcareHospitalManagement.Application.DTOs.Doctor;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Doctors
{
    public class UpdateDoctorHandler(IDoctorUnitOfWork uow, IMapper mapper)
        : IRequestHandler<UpdateDoctorCommand, ApiResponse<UpdateDoctorRequestDto>>
    {
        public async Task<ApiResponse<UpdateDoctorRequestDto>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await uow.Doctors.GetQueryable()
                .FirstOrDefaultAsync(d => d.Id == request.DoctorId && !d.IsDeleted, cancellationToken);

            if (doctor == null)
                return ApiResponse<UpdateDoctorRequestDto>.FailResponse("Doctor not found.");

            // 🔎 AutoMapper দিয়ে DTO → Entity map করা
            mapper.Map(request.Dto, doctor);

            await uow.Doctors.UpdateAsync(doctor, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            // 🔎 AutoMapper দিয়ে Entity → DTO ফেরত দেওয়া
            var updatedDto = mapper.Map<UpdateDoctorRequestDto>(doctor);

            return ApiResponse<UpdateDoctorRequestDto>.SuccessResponse(updatedDto, "Doctor profile updated successfully.");
        }
    }
}
