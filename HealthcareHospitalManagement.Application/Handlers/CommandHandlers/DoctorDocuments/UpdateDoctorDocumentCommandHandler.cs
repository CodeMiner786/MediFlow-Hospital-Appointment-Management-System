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
    public class UpdateDoctorDocumentCommandHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<UpdateDoctorDocumentCommand, ApiResponseDto<DoctorDocumentDto>>
    {
        public async Task<ApiResponseDto<DoctorDocumentDto>> Handle(
            UpdateDoctorDocumentCommand request, CancellationToken ct)
        {
            var entity = await uow.DoctorDocuments.GetByIdAsync(request.DocumentId, ct);

            if (entity is null)
                return ApiResponseDto<DoctorDocumentDto>.FailResponse(
                    $"Document Id '{request.DocumentId}' পাওয়া যায়নি।");

            // null-safe mapping — শুধু non-null field update হবে
            mapper.Map(request.Dto, entity);

            await uow.DoctorDocuments.UpdateAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            var domainResponse = ApiResponse<Domain.Entities.Doctor.DoctorDocument>
                .SuccessResponse(entity, "Document আপডেট হয়েছে।");

            var mapped = mapper.Map<DoctorDocumentDto>(domainResponse.Data);
            return ApiResponseMapper.ToDto(
                ApiResponse<DoctorDocumentDto>.SuccessResponse(mapped, domainResponse.Message));
        }
    }

}
