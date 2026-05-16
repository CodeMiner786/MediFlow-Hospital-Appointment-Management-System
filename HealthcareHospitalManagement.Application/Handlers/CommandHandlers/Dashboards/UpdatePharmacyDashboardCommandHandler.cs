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
    public sealed class UpdatePharmacyDashboardCommandHandler(
    IDashboardUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<UpdatePharmacyDashboardCommand, ApiResponseDto<PharmacyDashboardResponseDto>>
    {
        public async Task<ApiResponseDto<PharmacyDashboardResponseDto>> Handle(
            UpdatePharmacyDashboardCommand request,
            CancellationToken cancellationToken)
        {
            var metric = await unitOfWork.PharmacyDashboardMetrics.GetByIdAsync(request.Id, cancellationToken);
            if (metric is null)
                return ApiResponseDto<PharmacyDashboardResponseDto>.Fail("Pharmacy ড্যাশবোর্ড মেট্রিক পাওয়া যায়নি।");

            if (request.PendingOrders.HasValue) metric.PendingOrders = request.PendingOrders.Value;
            if (request.CompletedToday.HasValue) metric.CompletedOrders = request.CompletedToday.Value;
            if (request.TodayRevenue.HasValue) metric.TodayRevenue = request.TodayRevenue.Value;
            if (request.MonthlyRevenue.HasValue) metric.MonthlyRevenue = request.MonthlyRevenue.Value;
            if (request.LowStockMedicines.HasValue) metric.LowStockMedicines = request.LowStockMedicines.Value;
            if (request.OutOfStockMedicines.HasValue) metric.OutOfStockMedicines = request.OutOfStockMedicines.Value;
            if (request.ExpiringMedicines.HasValue) metric.ExpiringThisMonth = request.ExpiringMedicines.Value;

            await unitOfWork.PharmacyDashboardMetrics.UpdateAsync(metric, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = mapper.Map<PharmacyDashboardResponseDto>(metric);
            return ApiResponseDto<PharmacyDashboardResponseDto>.Ok(dto, "Pharmacy ড্যাশবোর্ড মেট্রিক সফলভাবে আপডেট হয়েছে।");
        }
    }

}
