using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Appointments;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Enums.Appointment;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Appointments
{
    public class RescheduleAppointmentHandler(IAppointmentUnitOfWork uow, IMapper mapper)
        : IRequestHandler<RescheduleAppointmentCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await uow.Appointments.GetByIdAsync(request.RequestDto.AppointmentId, cancellationToken);

            if (appointment is null)
            {
                return ApiResponse<bool>.FailResponse(
                    "Appointment not found.",
                    [$"No appointment exists with Id {request.RequestDto.AppointmentId}"]
                );
            }

            // ✅ AutoMapper ব্যবহার করে DTO → Entity update
            mapper.Map(request.RequestDto, appointment);

            // Business logic: Status পরিবর্তন করে Rescheduled করা হচ্ছে
            appointment.Status = AppointmentStatus.Rescheduled;

            await uow.Appointments.UpdateAsync(appointment, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return ApiResponse<bool>.SuccessResponse(true, "Appointment rescheduled successfully.");
        }
    }
}
