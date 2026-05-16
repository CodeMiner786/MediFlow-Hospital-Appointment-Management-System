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
    public class CreateLabOrderHandler(ILabUnitOfWork uow, IMapper mapper)
        : IRequestHandler<CreateLabOrderCommand, ApiResponse<Guid>>
    {
        public async Task<ApiResponse<Guid>> Handle(CreateLabOrderCommand request, CancellationToken cancellationToken)
        {
            // DTO → Entity mapping
            var labOrder = mapper.Map<LabOrder>(request.RequestDto);

            // Items mapping (DTO → Entity)
            labOrder.Items = mapper.Map<List<LabOrderItem>>(request.RequestDto.Items);

            // Save to DB
            await uow.LabOrders.AddAsync(labOrder, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return ApiResponse<Guid>.SuccessResponse(labOrder.Id, "Lab order created successfully.");
        }
    }
}
