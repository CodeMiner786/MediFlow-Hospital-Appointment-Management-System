using HealthcareHospitalManagement.Application.Commands.DoctorDocuments;
using HealthcareHospitalManagement.Application.Helpers.MapperService;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorDocuments
{
    public class SoftDeleteDoctorDocumentCommandHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<SoftDeleteDoctorDocumentCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            SoftDeleteDoctorDocumentCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorDocuments.GetByIdAsync(request.DocumentId, ct);

            if (entity is null)
                return ApiResponseDto<bool>.FailResponse(
                    $"Document Id '{request.DocumentId}' পাওয়া যায়নি।");

            await uow.DoctorDocuments.SoftDeleteAsync(request.DocumentId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseMapper.ToDto(
                ApiResponse<bool>.SuccessResponse(true, "Document সফট-ডিলিট হয়েছে।"));
        }
    }

}
