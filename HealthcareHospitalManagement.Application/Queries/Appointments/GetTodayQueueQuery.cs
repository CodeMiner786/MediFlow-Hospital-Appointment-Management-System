using HealthcareHospitalManagement.Application.DTOs.Appointment;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Queries.Appointments
{
    // আজকের Queue এর Appointment লিস্ট আনার জন্য Query
    public record GetTodayQueueQuery(DateTime Date)
        : IRequest<ApiResponse<List<TodayQueueItemResponseDto>>>;
}
