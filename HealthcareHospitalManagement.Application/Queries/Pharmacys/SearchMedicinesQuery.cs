using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Pharmacys
{
    public sealed record SearchMedicinesQuery(MedicineSearchRequestDto Dto)
    : IRequest<ApiResponseDto<PagedResultDto<MedicineResponseDto>>>;


}
