using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using MediatR;

namespace HealthcareHospitalManagement.Application.Queries.Pharmacys
{
    public sealed record GetPrescriptionsByPatientQuery(Guid PatientId, int PageNumber = 1, int PageSize = 10)
     : IRequest<ApiResponseDto<PagedResultDto<PrescriptionResponseDto>>>;

}
