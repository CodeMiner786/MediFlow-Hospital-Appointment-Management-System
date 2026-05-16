using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using HealthcareHospitalManagement.Application.Queries.Pharmacys;
using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Pharmacys
{
    public sealed class GetPrescriptionsByPatientQueryHandler(IPharmacyUnitOfWork uow, IMapper mapper)
     : IRequestHandler<GetPrescriptionsByPatientQuery, ApiResponseDto<PagedResultDto<PrescriptionResponseDto>>>
    {
        public async Task<ApiResponseDto<PagedResultDto<PrescriptionResponseDto>>> Handle(
            GetPrescriptionsByPatientQuery request, CancellationToken ct)
        {
            var allData = (await uow.Prescriptions.GetPrescriptionsByPatientIdAsync(request.PatientId, ct)).ToList();
            var totalRecords = allData.Count;

            var pagedData = allData
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList(); // 🔹 ToList() করা হয়েছে যাতে IEnumerable lazy না থাকে

            var pagedResponse = PagedResponse<Domain.Entities.Pharmacy.Prescription>.Create(
                data: pagedData,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                totalRecords: totalRecords);

            var result = pagedResponse.ToMappedPagedResult<
                Domain.Entities.Pharmacy.Prescription,
                PrescriptionResponseDto>(mapper);

            return ApiResponseDto<PagedResultDto<PrescriptionResponseDto>>.Ok(result);
        }
    }

}
