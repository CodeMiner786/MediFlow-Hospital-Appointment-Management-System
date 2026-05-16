using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.Labs;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Labs
{
    public class CreateLabOrderItemHandler(ILabUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateLabOrderItemCommand, ApiResponse<Guid>>
    {
        public async Task<ApiResponse<Guid>> Handle(CreateLabOrderItemCommand request, CancellationToken cancellationToken)
        {
            var labOrderItem = mapper.Map<LabOrderItem>(request.RequestDto);

            await uow.LabOrderItems.AddAsync(labOrderItem, cancellationToken);
            var saved = await uow.SaveChangesAsync(cancellationToken);

            if (saved > 0)
            {
                return ApiResponse<Guid>.SuccessResponse(labOrderItem.Id, "Lab order item created successfully.");
            }

            return ApiResponse<Guid>.FailResponse(
                    "Failed to create lab order item.",
                    ["No changes were saved to the database."]
            );


        }
    }

}
