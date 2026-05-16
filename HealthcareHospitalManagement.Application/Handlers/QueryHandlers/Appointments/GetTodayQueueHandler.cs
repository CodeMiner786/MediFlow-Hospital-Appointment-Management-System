using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Appointment;
using HealthcareHospitalManagement.Application.Queries.Appointments;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Entities.Appointment;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Appointments
{
    public class GetTodayQueueHandler(IAppointmentUnitOfWork uow, IMapper mapper)
        : IRequestHandler<GetTodayQueueQuery, ApiResponse<List<TodayQueueItemResponseDto>>>
    {
        public async Task<ApiResponse<List<TodayQueueItemResponseDto>>> Handle(GetTodayQueueQuery request, CancellationToken cancellationToken)
        {
            var appointments = new List<AppointmentEntity>();

            // ✅ Streaming data কে list এ convert করা হচ্ছে
            await foreach (var appointment in uow.Appointments.GetDoctorAppointmentsByDateStream(Guid.Empty, request.Date)
                .WithCancellation(cancellationToken))
            {
                appointments.Add(appointment);
            }

            if (appointments.Count == 0)
            {
                return ApiResponse<List<TodayQueueItemResponseDto>>.FailResponse(
                    "No appointments found for today.",
                    [$"No appointments scheduled on {request.Date:yyyy-MM-dd}"]
                );
            }


            // AutoMapper দিয়ে Entity → DTO convert
            var dtoList = mapper.Map<List<TodayQueueItemResponseDto>>(appointments);

            return ApiResponse<List<TodayQueueItemResponseDto>>.SuccessResponse(dtoList, "Today's queue retrieved successfully.");
        }
    }
}
