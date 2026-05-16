using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Dashboard;
using HealthcareHospitalManagement.Application.Queries.Dashboards;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Dashboards
{
    [ApiController]
    [Route("api/dashboard/patient")]
    [Authorize(Roles = "Admin,Patient")]
    public sealed class PatientDashboardController(IMediator mediator) : ControllerBase
    {
        /// <summary>Patient ড্যাশবোর্ড ডেটা দেখুন (read-only)।</summary>
        [HttpGet("{patientId:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<PatientDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<PatientDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPatientDashboard(
            Guid patientId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetPatientDashboardQuery(patientId), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}
