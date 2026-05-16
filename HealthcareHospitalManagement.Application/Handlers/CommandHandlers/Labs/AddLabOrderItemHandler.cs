using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Labs;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Labs
{
    public class AddLabOrderItemHandler(ILabUnitOfWork uow, IMapper mapper)
    : IRequestHandler<AddLabOrderItemCommand, ApiResponse<Guid>>
    {
        public async Task<ApiResponse<Guid>> Handle(AddLabOrderItemCommand request, CancellationToken cancellationToken)
        {
            // Step 1 — LabOrder আছে কিনা check করো
            var labOrder = await uow.LabOrders.GetByIdAsync(request.LabOrderId, cancellationToken)
                ?? throw new KeyNotFoundException($"Lab order not found with id: {request.LabOrderId}");

            // Step 2 — DTO → LabOrderItem Entity
            var item = mapper.Map<LabOrderItem>(request.RequestDto);

            // Step 3 — Foreign Key set করো
            item.LabOrderId = labOrder.Id;

            // Step 4 — Database এ save করো
            await uow.LabOrderItems.AddAsync(item, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            // Step 5 — নতুন Item এর Id return করো
            return ApiResponse<Guid>.SuccessResponse(item.Id, "Lab order item added successfully.");
        }
    }
}
