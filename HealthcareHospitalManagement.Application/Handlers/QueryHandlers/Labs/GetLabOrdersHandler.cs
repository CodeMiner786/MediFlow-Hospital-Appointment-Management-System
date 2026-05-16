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
    public class GetLabOrdersHandler(ILabUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetLabOrdersQuery, ApiResponse<List<LabOrderResponseDto>>>
    {
        public async Task<ApiResponse<List<LabOrderResponseDto>>> Handle(GetLabOrdersQuery request, CancellationToken cancellationToken)
        {
            var filter = request.FilterDto;

            var labOrders = await uow.LabOrders.GetFilteredOrdersAsync(
                filter.PatientId,
                filter.DoctorId,
                filter.Priority,
                filter.IsPaid,
                filter.DateFrom,
                filter.DateTo,
                filter.PageNumber,
                filter.PageSize,
                cancellationToken
            );

            // Analyzer অনুযায়ী Count == 0
            if (labOrders.Count == 0)
            {
                return ApiResponse<List<LabOrderResponseDto>>.FailResponse(
                        "No lab orders found.",
                        ["No lab orders match the given filter criteria."]
                );

            }

            var dtoList = mapper.Map<List<LabOrderResponseDto>>(labOrders);

            return ApiResponse<List<LabOrderResponseDto>>.SuccessResponse(dtoList, "Lab orders retrieved successfully.");
        }
    }

}
