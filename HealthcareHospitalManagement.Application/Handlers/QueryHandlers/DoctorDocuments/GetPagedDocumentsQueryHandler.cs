using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
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
    public class GetPagedDocumentsQueryHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetPagedDocumentsQuery, ApiResponseDto<PagedResultDto<DoctorDocumentDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<DoctorDocumentDto>>> Handle(
            GetPagedDocumentsQuery request, CancellationToken ct)
        {
            // Step 1: Repository থেকে Domain PagedResponse<Entity> আনো
            var pagedEntity = await uow.DoctorDocuments.GetPagedAsync(
                request.PageNumber, request.PageSize, ct);

            // Step 2: PagingExtensions দিয়ে Entity → DTO map করো
            //         এটা তোমার ToMappedPagedResult() extension method
            var pagedResult = pagedEntity.ToMappedPagedResult<
                Domain.Entities.Doctor.DoctorDocument, DoctorDocumentDto>(mapper);

            // Step 3: Domain ApiResponse বানাও
            var domainResponse = ApiResponse<PagedResultDto<DoctorDocumentDto>>
                .SuccessResponse(pagedResult);

            // Step 4: ApiResponseMapper দিয়ে Domain ApiResponse → Application ApiResponseDto
            return ApiResponseMapper.ToDto(domainResponse);
        }
    }

}
