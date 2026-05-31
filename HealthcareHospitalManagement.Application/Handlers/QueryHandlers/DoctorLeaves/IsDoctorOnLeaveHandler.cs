using HealthcareHospitalManagement.Application.Queries.DoctorLeaves;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.DoctorLeaves
{
    public class IsDoctorOnLeaveHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<IsDoctorOnLeaveQuery, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            IsDoctorOnLeaveQuery request, CancellationToken ct)
        {
            var result = await uow.DoctorLeaves.IsDoctorOnLeaveAsync(request.DoctorId, request.Date, ct);
            return ApiResponseDto<bool>.SuccessResponse(result);
        }
    }

}
