using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Appointments;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Entities.Appointment;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Appointments
{
    public class BookAppointmentHandler(IAppointmentUnitOfWork uow, IMapper mapper)
        : IRequestHandler<BookAppointmentCommand, ApiResponse<Guid>>
    {
        public async Task<ApiResponse<Guid>> Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
        {
            // Doctor already booked check (example logic)
            bool doctorAlreadyBooked = await uow.Appointments
                .IsDoctorBookedAsync(request.RequestDto.DoctorId, request.RequestDto.AppointmentDate, request.RequestDto.StartTime, cancellationToken);

            if (doctorAlreadyBooked)
            {
                // ❌ FailResponse
                return ApiResponse<Guid>.FailResponse(
                    "Doctor not available at this time.",
                    [$"Doctor already has an appointment at {request.RequestDto.StartTime}"]
);

            }

            // ✅ SuccessResponse
            var entity = mapper.Map<AppointmentEntity>(request.RequestDto);

            await uow.Appointments.AddAsync(entity, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return ApiResponse<Guid>.SuccessResponse(entity.Id, "Appointment booked successfully.");
        }
    }
}
