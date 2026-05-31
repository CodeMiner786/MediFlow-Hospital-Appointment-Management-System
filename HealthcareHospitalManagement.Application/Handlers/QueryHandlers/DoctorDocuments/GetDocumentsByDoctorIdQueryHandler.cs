using AutoMapper;
using HealthcareHospitalManagement.Application.DTOs.DoctorDocument;
using HealthcareHospitalManagement.Application.Helpers.MapperService;
using HealthcareHospitalManagement.Application.Helpers.Stream;
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
    public class GetDocumentsByDoctorIdQueryHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<GetDocumentsByDoctorIdQuery, ApiResponseDto<List<DoctorDocumentDto>>>
    {
        public async Task<ApiResponseDto<List<DoctorDocumentDto>>> Handle(
            GetDocumentsByDoctorIdQuery request, CancellationToken ct)
        {
            // IAsyncEnumerable → List
            var entities = await StreamHelper.ToList(
                uow.DoctorDocuments.GetDocumentsByDoctorIdStream(request.DoctorId), ct);

            var domainResponse = ApiResponse<List<DoctorDocumentDto>>
                .SuccessResponse(mapper.Map<List<DoctorDocumentDto>>(entities));

            return ApiResponseMapper.ToDto(domainResponse);
        }
    }

}
