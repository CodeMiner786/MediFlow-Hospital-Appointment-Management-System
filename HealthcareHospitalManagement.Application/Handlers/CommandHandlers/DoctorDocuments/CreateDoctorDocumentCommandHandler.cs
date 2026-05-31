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
    public class CreateDoctorDocumentCommandHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateDoctorDocumentCommand, ApiResponseDto<DoctorDocumentDto>>
    {
        public async Task<ApiResponseDto<DoctorDocumentDto>> Handle(
            CreateDoctorDocumentCommand request, CancellationToken ct)
        {
            var doctorExists = await uow.Doctors.ExistsAsync(d => d.Id == request.Dto.DoctorId, ct);

            if (!doctorExists)
                return ApiResponseDto<DoctorDocumentDto>.FailResponse(
                    $"Doctor Id '{request.Dto.DoctorId}' পাওয়া যায়নি।");

            var entity = mapper.Map<Domain.Entities.Doctor.DoctorDocument>(request.Dto);

            await uow.DoctorDocuments.AddAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            // Domain ApiResponse → DTO (ApiResponseMapper দিয়ে)
            var domainResponse = ApiResponse<Domain.Entities.Doctor.DoctorDocument>
                .SuccessResponse(entity, "Document তৈরি হয়েছে।");

            var mapped = mapper.Map<DoctorDocumentDto>(domainResponse.Data);
            return ApiResponseMapper.ToDto(
                ApiResponse<DoctorDocumentDto>.SuccessResponse(mapped, domainResponse.Message));
        }
    }

}
