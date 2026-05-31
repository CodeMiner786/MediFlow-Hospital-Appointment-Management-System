using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.DoctorDocument;
using HealthcareHospitalManagement.Application.Helpers.MapperService;
using HealthcareHospitalManagement.Application.Queries.DoctorDocuments;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.DoctorDocuments
{
    public class GetDocumentByIdQueryHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetDocumentByIdQuery, ApiResponseDto<DoctorDocumentDto>>
    {
        public async Task<ApiResponseDto<DoctorDocumentDto>> Handle(
            GetDocumentByIdQuery request, CancellationToken ct)
        {
            var entity = await uow.DoctorDocuments.GetByIdAsync(request.DocumentId, ct);

            if (entity is null)
                return ApiResponseDto<DoctorDocumentDto>.FailResponse(
                    $"Document Id '{request.DocumentId}' পাওয়া যায়নি।");

            var domainResponse = ApiResponse<DoctorDocumentDto>
                .SuccessResponse(mapper.Map<DoctorDocumentDto>(entity));

            return ApiResponseMapper.ToDto(domainResponse);
        }
    }

}
