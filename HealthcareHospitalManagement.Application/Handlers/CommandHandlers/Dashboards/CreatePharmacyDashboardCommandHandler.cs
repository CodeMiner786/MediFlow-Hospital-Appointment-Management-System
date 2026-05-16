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
    public sealed class CreatePharmacyDashboardCommandHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<CreatePharmacyDashboardCommand, ApiResponseDto<PharmacyDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<PharmacyDashboardResponseDto>> Handle(
            CreatePharmacyDashboardCommand request,
            CancellationToken cancellationToken)
        {
            var existing = await unitOfWork.PharmacyDashboardMetrics.GetByPharmacyAndDateAsync(request.PharmacyProfileId, request.MetricDate);
            if (existing is not null)
                return ApiResponseDto<PharmacyDashboardResponseDto>.Fail("এই ফার্মেসির এই তারিখের মেট্রিক ইতিমধ্যে বিদ্যমান।");

            var metric = new PharmacyDashboardMetric
            {
                PharmacyProfileId = request.PharmacyProfileId,
                MetricDate = request.MetricDate,
                PendingOrders = request.PendingOrders,
                CompletedOrders = request.CompletedToday,
                TodayRevenue = request.TodayRevenue,
                MonthlyRevenue = request.MonthlyRevenue,
                LowStockMedicines = request.LowStockMedicines,
                OutOfStockMedicines = request.OutOfStockMedicines,
                ExpiringThisMonth = request.ExpiringMedicines,
            };

            await unitOfWork.PharmacyDashboardMetrics.AddAsync(metric, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = mapper.Map<PharmacyDashboardResponseDto>(metric);
            return ApiResponseDto<PharmacyDashboardResponseDto>.Ok(dto, "Pharmacy ড্যাশবোর্ড মেট্রিক সফলভাবে তৈরি হয়েছে।");
        }
    }


}
