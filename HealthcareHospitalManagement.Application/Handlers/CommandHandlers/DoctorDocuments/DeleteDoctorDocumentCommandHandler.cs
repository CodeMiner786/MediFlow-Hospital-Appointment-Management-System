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
    public class DeleteDoctorDocumentCommandHandler(IDoctorUnitOfWork uow)
    : IRequestHandler<DeleteDoctorDocumentCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            DeleteDoctorDocumentCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorDocuments.GetByIdAsync(request.DocumentId, ct);

            if (entity is null)
                return ApiResponseDto<bool>.FailResponse(
                    $"Document Id '{request.DocumentId}' পাওয়া যায়নি।");

            await uow.DoctorDocuments.DeleteAsync(request.DocumentId, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseMapper.ToDto(
                ApiResponse<bool>.SuccessResponse(true, "Document ডিলিট হয়েছে।"));
        }
    }

}
