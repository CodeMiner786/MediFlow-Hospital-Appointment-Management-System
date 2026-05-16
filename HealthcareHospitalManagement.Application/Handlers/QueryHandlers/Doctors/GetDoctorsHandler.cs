using AutoMapper;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Doctor;
using HealthcareHospitalManagement.Application.Queries.Doctors;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Doctors
{
    public class GetDoctorsHandler(IDoctorUnitOfWork uow, IMapper mapper)
        : IRequestHandler<GetDoctorsQuery, PagedResultDto<DoctorSummaryResponseDto>>
    {
        public async Task<PagedResultDto<DoctorSummaryResponseDto>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            var queryable = uow.Doctors.GetQueryable();

            // 🔎 Filtering logic
            if (!string.IsNullOrWhiteSpace(request.Dto.Name))
                queryable = queryable.Where(d => d.FirstName.Contains(request.Dto.Name) || d.LastName.Contains(request.Dto.Name));

            if (request.Dto.Specialization.HasValue)
                queryable = queryable.Where(d => d.Specialization == request.Dto.Specialization.Value);

            if (request.Dto.DepartmentId.HasValue)
                queryable = queryable.Where(d => d.DepartmentId == request.Dto.DepartmentId.Value);

            if (request.Dto.IsAvailableNow.HasValue)
                queryable = queryable.Where(d => d.IsAvailableNow == request.Dto.IsAvailableNow.Value);

            // 🔎 Pagination
            var totalCount = await queryable.CountAsync(cancellationToken);
            var doctors = await queryable
                .Skip((request.Dto.PageNumber - 1) * request.Dto.PageSize)
                .Take(request.Dto.PageSize)
                .ToListAsync(cancellationToken);

            // 🔎 Mapping entity → DTO
            var mappedDoctors = mapper.Map<List<DoctorSummaryResponseDto>>(doctors);

            return new PagedResultDto<DoctorSummaryResponseDto>(mappedDoctors, totalCount, request.Dto.PageNumber, request.Dto.PageSize);
        }
    }
}
