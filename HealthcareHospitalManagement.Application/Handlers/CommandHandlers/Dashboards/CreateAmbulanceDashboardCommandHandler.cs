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
    public sealed class CreateAmbulanceDashboardCommandHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<CreateAmbulanceDashboardCommand, ApiResponseDto<AmbulanceDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<AmbulanceDashboardResponseDto>> Handle(
            CreateAmbulanceDashboardCommand request,
            CancellationToken cancellationToken)
        {
            var existing = await unitOfWork.AmbulanceDashboardMetrics.GetByProviderAndDateAsync(request.ProviderId, request.MetricDate);
            if (existing is not null)
                return ApiResponseDto<AmbulanceDashboardResponseDto>.Fail("এই প্রোভাইডারের এই তারিখের মেট্রিক ইতিমধ্যে বিদ্যমান।");

            var metric = new AmbulanceDashboardMetric
            {
                ProviderId = request.ProviderId,
                MetricDate = request.MetricDate,
                TotalVehicles = request.TotalVehicles,
                AvailableVehicles = request.AvailableVehicles,
                TotalBookingsToday = request.TodayBookings,
                CompletedBookings = request.CompletedToday,
                TodayEarnings = request.TodayRevenue,
                MonthlyEarnings = request.MonthlyRevenue,
                WalletBalance = request.WalletBalance,
                AverageRating = request.AverageRating,
            };

            await unitOfWork.AmbulanceDashboardMetrics.AddAsync(metric, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = mapper.Map<AmbulanceDashboardResponseDto>(metric);
            return ApiResponseDto<AmbulanceDashboardResponseDto>.Ok(dto, "Ambulance ড্যাশবোর্ড মেট্রিক সফলভাবে তৈরি হয়েছে।");
        }
    }

}
