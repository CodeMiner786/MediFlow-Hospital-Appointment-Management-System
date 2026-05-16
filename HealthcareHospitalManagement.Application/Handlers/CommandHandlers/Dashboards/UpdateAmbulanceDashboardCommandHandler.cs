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
    public sealed class UpdateAmbulanceDashboardCommandHandler(
        IDashboardUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<UpdateAmbulanceDashboardCommand, ApiResponseDto<AmbulanceDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<AmbulanceDashboardResponseDto>> Handle(
            UpdateAmbulanceDashboardCommand request,
            CancellationToken cancellationToken)
        {
            var metric = await unitOfWork.AmbulanceDashboardMetrics.GetByIdAsync(request.Id, cancellationToken);
            if (metric is null)
                return ApiResponseDto<AmbulanceDashboardResponseDto>.Fail("Ambulance ড্যাশবোর্ড মেট্রিক পাওয়া যায়নি।");

            if (request.TotalVehicles.HasValue) metric.TotalVehicles = request.TotalVehicles.Value;
            if (request.AvailableVehicles.HasValue) metric.AvailableVehicles = request.AvailableVehicles.Value;
            if (request.TodayBookings.HasValue) metric.TotalBookingsToday = request.TodayBookings.Value;
            if (request.CompletedToday.HasValue) metric.CompletedBookings = request.CompletedToday.Value;
            if (request.TodayRevenue.HasValue) metric.TodayEarnings = request.TodayRevenue.Value;
            if (request.MonthlyRevenue.HasValue) metric.MonthlyEarnings = request.MonthlyRevenue.Value;
            if (request.WalletBalance.HasValue) metric.WalletBalance = request.WalletBalance.Value;
            if (request.AverageRating.HasValue) metric.AverageRating = request.AverageRating.Value;

            await unitOfWork.AmbulanceDashboardMetrics.UpdateAsync(metric, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = mapper.Map<AmbulanceDashboardResponseDto>(metric);
            return ApiResponseDto<AmbulanceDashboardResponseDto>.Ok(dto, "Ambulance ড্যাশবোর্ড মেট্রিক সফলভাবে আপডেট হয়েছে।");
        }
    }
}
