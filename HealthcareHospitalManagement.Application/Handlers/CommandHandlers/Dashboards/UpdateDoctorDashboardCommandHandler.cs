using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Dashboards;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Dashboard;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Dashboards
{
    public sealed class UpdateDoctorDashboardCommandHandler(
        IDashboardUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<UpdateDoctorDashboardCommand, ApiResponseDto<DoctorDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<DoctorDashboardResponseDto>> Handle(
            UpdateDoctorDashboardCommand request,
            CancellationToken cancellationToken)
        {
            var metric = await unitOfWork.DoctorDashboardMetrics.GetByIdAsync(request.Id, cancellationToken);
            if (metric is null)
                return ApiResponseDto<DoctorDashboardResponseDto>.Fail("Doctor ড্যাশবোর্ড মেট্রিক পাওয়া যায়নি।");

            if (request.TodayAppointments.HasValue) metric.TotalAppointmentsToday = request.TodayAppointments.Value;
            if (request.PendingAppointments.HasValue) metric.PendingAppointments = request.PendingAppointments.Value;
            if (request.CompletedToday.HasValue) metric.CompletedAppointments = request.CompletedToday.Value;
            if (request.CancelledToday.HasValue) metric.CancelledAppointments = request.CancelledToday.Value;
            if (request.TotalPatients.HasValue) metric.TotalPatientsToday = request.TotalPatients.Value;
            if (request.TodayEarnings.HasValue) metric.TotalRevenueToday = request.TodayEarnings.Value;
            if (request.MonthlyEarnings.HasValue) metric.MonthlyRevenue = request.MonthlyEarnings.Value;
            if (request.AverageRating.HasValue) metric.AverageRating = request.AverageRating.Value;
            if (request.TotalReviews.HasValue) metric.TotalReviews = request.TotalReviews.Value;
            if (request.ActiveTelemedicineSessions.HasValue) metric.TelemedicineAppointments = request.ActiveTelemedicineSessions.Value;

            await unitOfWork.DoctorDashboardMetrics.UpdateAsync(metric, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = mapper.Map<DoctorDashboardResponseDto>(metric);
            return ApiResponseDto<DoctorDashboardResponseDto>.Ok(dto, "Doctor ড্যাশবোর্ড মেট্রিক সফলভাবে আপডেট হয়েছে।");
        }
    }
}
