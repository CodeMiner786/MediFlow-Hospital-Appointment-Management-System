using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Pharmacys;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Pharmacys
{
    public sealed class PlaceMedicineOrderCommandHandler(
     IPharmacyUnitOfWork uow,
     IMapper mapper)
     : IRequestHandler<PlaceMedicineOrderCommand, ApiResponseDto<MedicineOrderResponseDto>>
    {
        public async Task<ApiResponseDto<MedicineOrderResponseDto>> Handle(
            PlaceMedicineOrderCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // ── ① প্রতিটা Medicine এর Price লোড করে TotalAmount হিসাব ──
            decimal totalAmount = 0;
            var orderItems = new List<MedicineOrderItem>();

            foreach (var item in dto.Items)
            {
                var medicine = await uow.Medicines.GetByIdAsync(item.MedicineId, cancellationToken);
                if (medicine is null)
                    return ApiResponseDto<MedicineOrderResponseDto>.Fail(
                        $"MedicineId '{item.MedicineId}' পাওয়া যায়নি।");

                var orderItem = new MedicineOrderItem
                {
                    MedicineId = item.MedicineId,
                    Quantity = item.Quantity,
                    UnitPrice = medicine.SellingPrice,
                    Discount = 0,
                    TotalPrice = medicine.SellingPrice * item.Quantity,
                };

                totalAmount += orderItem.TotalPrice;
                orderItems.Add(orderItem);
            }

            // ── ② AutoMapper দিয়ে Order Entity তৈরি ──
            var order = mapper.Map<MedicineOrder>(dto);

            // ── ③ Business Logic fields ──
            order.OrderCode = $"ORD-{Random.Shared.Next(10000, 99999)}";
            order.OrderDate = DateTime.UtcNow;
            order.Status = MedicineOrderStatus.Placed;
            order.TotalAmount = totalAmount;
            order.Items = orderItems;

            await uow.MedicineOrders.AddAsync(order, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<MedicineOrderResponseDto>(order);
            return ApiResponseDto<MedicineOrderResponseDto>.Ok(result, "অর্ডার সফলভাবে Place হয়েছে।");
        }
    }

}
