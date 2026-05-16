using HealthcareHospitalManagement.Application.Commands.Pharmacys;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Pharmacys;
public sealed class CancelMedicineOrderCommandHandler(IPharmacyUnitOfWork uow)
    : IRequestHandler<CancelMedicineOrderCommand, ApiResponseDto<bool>>
{
    public async Task<ApiResponseDto<bool>> Handle(
        CancelMedicineOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await uow.MedicineOrders.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
            return ApiResponseDto<bool>.Fail(
                $"OrderId '{request.OrderId}' পাওয়া যায়নি।");

        // ── Delivered হলে Cancel করা যাবে না ──
        if (order.Status == MedicineOrderStatus.Delivered)
            return ApiResponseDto<bool>.Fail("Delivered অর্ডার Cancel করা যাবে না।");

        order.Status = MedicineOrderStatus.Cancelled;
        order.UpdatedAt = DateTime.UtcNow;

        await uow.MedicineOrders.UpdateAsync(order, cancellationToken);
        await uow.SaveChangesAsync(cancellationToken);

        return ApiResponseDto<bool>.Ok(true, "অর্ডার সফলভাবে Cancel করা হয়েছে।");
    }
}
