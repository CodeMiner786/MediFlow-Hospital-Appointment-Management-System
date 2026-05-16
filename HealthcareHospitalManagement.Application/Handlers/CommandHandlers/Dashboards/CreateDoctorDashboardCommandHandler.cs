using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Dashboards;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Dashboard;
using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Dashboards
{
    public sealed class CreateDoctorDashboardCommandHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<CreateDoctorDashboardCommand, ApiResponseDto<DoctorDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<DoctorDashboardResponseDto>> Handle(
            CreateDoctorDashboardCommand request,
            CancellationToken cancellationToken)
        {
            var existing = await unitOfWork.DoctorDashboardMetrics.GetByDoctorAndDateAsync(request.DoctorId, request.MetricDate);
            if (existing is not null)
                return ApiResponseDto<DoctorDashboardResponseDto>.Fail("এই ডাক্তারের এই তারিখের মেট্রিক ইতিমধ্যে বিদ্যমান।");

            var metric = new DoctorDashboardMetric
            {
                DoctorId = request.DoctorId,
                MetricDate = request.MetricDate,
                TotalAppointmentsToday = request.TodayAppointments,
                PendingAppointments = request.PendingAppointments,
                CompletedAppointments = request.CompletedToday,
                CancelledAppointments = request.CancelledToday,
                TotalPatientsToday = request.TotalPatients,
                TotalRevenueToday = request.TodayEarnings,
                MonthlyRevenue = request.MonthlyEarnings,
                AverageRating = request.AverageRating,
                TotalReviews = request.TotalReviews,
                TelemedicineAppointments = request.ActiveTelemedicineSessions,
            };

            await unitOfWork.DoctorDashboardMetrics.AddAsync(metric, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = mapper.Map<DoctorDashboardResponseDto>(metric);
            return ApiResponseDto<DoctorDashboardResponseDto>.Ok(dto, "Doctor ড্যাশবোর্ড মেট্রিক সফলভাবে তৈরি হয়েছে।");
        }
    }

}
