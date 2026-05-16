using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.Lab;
using HealthcareHospitalManagement.Application.Queries.Labs;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Labs
{
    public class GetLabOrderItemsHandler(ILabUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetLabOrderItemsQuery, ApiResponse<List<LabOrderItemResponseDto>>>
    {
        public async Task<ApiResponse<List<LabOrderItemResponseDto>>> Handle(
            GetLabOrderItemsQuery request,
            CancellationToken cancellationToken)
        {
            // Step 1 — LabOrder আছে কিনা check করো
            var labOrder = await uow.LabOrders.GetByIdAsync(request.LabOrderId, cancellationToken)
                ?? throw new KeyNotFoundException($"Lab order not found with id: {request.LabOrderId}");

            // Step 2 — সেই Order এর সব Items নিয়ে আসো
            var items = await uow.LabOrderItems.GetByLabOrderIdAsync(request.LabOrderId, cancellationToken);

            // Step 3 — Entity → ResponseDto map করো
            var result = mapper.Map<List<LabOrderItemResponseDto>>(items);

            return ApiResponse<List<LabOrderItemResponseDto>>.SuccessResponse(result, "Lab order items retrieved successfully.");
        }
    }
}
