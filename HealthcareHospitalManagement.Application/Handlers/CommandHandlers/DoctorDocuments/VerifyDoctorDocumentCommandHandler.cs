using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.DoctorDocuments;
using HealthcareHospitalManagement.Application.DTOs.DoctorDocument;
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
    public class VerifyDoctorDocumentCommandHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<VerifyDoctorDocumentCommand, ApiResponseDto<DoctorDocumentDto>>
    {
        public async Task<ApiResponseDto<DoctorDocumentDto>> Handle(
            VerifyDoctorDocumentCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorDocuments.GetByIdAsync(request.DocumentId, ct);

            if (entity is null)
                return ApiResponseDto<DoctorDocumentDto>.FailResponse(
                    $"Document Id '{request.DocumentId}' পাওয়া যায়নি।");

            entity.IsVerified = request.Dto.IsVerified;
            entity.VerifiedBy = request.Dto.VerifiedBy;
            entity.Notes = request.Dto.Notes;
            entity.VerifiedAt = request.Dto.IsVerified ? DateTime.UtcNow : null;

            await uow.DoctorDocuments.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            var msg = request.Dto.IsVerified ? "Document ভেরিফাই হয়েছে।" : "Document আন-ভেরিফাই হয়েছে।";

            var domainResponse = ApiResponse<Domain.Entities.Doctor.DoctorDocument>
                .SuccessResponse(entity, msg);

            var mapped = mapper.Map<DoctorDocumentDto>(domainResponse.Data);
            return ApiResponseMapper.ToDto(
                ApiResponse<DoctorDocumentDto>.SuccessResponse(mapped, domainResponse.Message));
        }
    }

}
