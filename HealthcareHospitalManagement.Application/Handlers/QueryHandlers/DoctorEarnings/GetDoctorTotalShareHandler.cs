using HealthcareHospitalManagement.Application.Queries.DoctorEarnings;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.DoctorEarnings
{
    public class GetDoctorTotalShareHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<GetDoctorTotalShareQuery, ApiResponseDto<decimal>>
    {
        public async Task<ApiResponseDto<decimal>> Handle(
            GetDoctorTotalShareQuery request, CancellationToken ct)
        {
            var total = await uow.DoctorEarnings
                           .GetTotalDoctorShareAmountAsync(request.DoctorId, request.OnlyPaid);

            return ApiResponseDto<decimal>.SuccessResponse(total,
                $"Total {(request.OnlyPaid ? "paid" : "all")} earnings fetched.");
        }
    }

}
