using HealthcareHospitalManagement.Application.Commands.Analytics;
using HealthcareHospitalManagement.Application.DTOs.Analytics;
using HealthcareHospitalManagement.Application.Queries.Analytics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Analytics
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AnalyticsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // ──────────────────────────────────────────────────────────────────
        // POST  api/analytics/dashboard/generate
        // ──────────────────────────────────────────────────────────────────
        [HttpPost("dashboard/generate")]
        public async Task<IActionResult> GenerateDashboardMetric(
            [FromQuery] DateOnly metricDate,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GenerateDashboardMetricCommand(metricDate), cancellationToken);
            return response?.Success == true ? Ok(response) : BadRequest(response);
        }

        // ──────────────────────────────────────────────────────────────────
        // POST  api/analytics/revenue/generate
        // ──────────────────────────────────────────────────────────────────
        [HttpPost("revenue/generate")]
        public async Task<IActionResult> GenerateRevenueReport(
            [FromBody] ReportFilterRequestDto dto,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GenerateRevenueReportCommand(dto), cancellationToken);
            return response?.Success == true ? Ok(response) : BadRequest(response);
        }

        // ──────────────────────────────────────────────────────────────────
        // GET  api/analytics/dashboard?metricDate=2025-01-15
        // ──────────────────────────────────────────────────────────────────
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardMetricByDate(
            [FromQuery] DateOnly metricDate,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetDashboardMetricsQuery(metricDate, null), cancellationToken);

            if (response is null || !response.Success)
            {
                if (response?.Message?.Contains("No dashboard metric") == true)
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        // ──────────────────────────────────────────────────────────────────
        // GET  api/analytics/dashboard/range?startDate=2025-01-01&endDate=2025-01-31
        // ──────────────────────────────────────────────────────────────────
        [HttpGet("dashboard/range")]
        public async Task<IActionResult> GetDashboardMetricsByDateRange(
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetDashboardMetricsQuery(startDate, endDate), cancellationToken);
            return response?.Success == true ? Ok(response) : BadRequest(response);
        }

        // ──────────────────────────────────────────────────────────────────
        // GET  api/analytics/revenue
        // ──────────────────────────────────────────────────────────────────
        [HttpGet("revenue")]
        public async Task<IActionResult> GetRevenueReport(
            [FromQuery] ReportFilterRequestDto dto,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetRevenueReportQuery(dto), cancellationToken);
            return response?.Success == true ? Ok(response) : BadRequest(response);
        }

        // ──────────────────────────────────────────────────────────────────
        // GET  api/analytics/doctors/performance
        // ──────────────────────────────────────────────────────────────────
        [HttpGet("doctors/performance")]
        public async Task<IActionResult> GetDoctorPerformanceReport(
            [FromQuery] ReportFilterRequestDto dto,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetDoctorPerformanceReportQuery(dto), cancellationToken);
            return response?.Success == true ? Ok(response) : BadRequest(response);
        }

        // ──────────────────────────────────────────────────────────────────
        // GET  api/analytics/patients/growth
        // ──────────────────────────────────────────────────────────────────
        [HttpGet("patients/growth")]
        public async Task<IActionResult> GetPatientGrowthReport(
            [FromQuery] ReportFilterRequestDto dto,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetPatientGrowthReportQuery(dto), cancellationToken);
            return response?.Success == true ? Ok(response) : BadRequest(response);
        }

        // ──────────────────────────────────────────────────────────────────
        // GET  api/analytics/departments/summary
        // ──────────────────────────────────────────────────────────────────
        [HttpGet("departments/summary")]
        public async Task<IActionResult> GetDepartmentSummaryReport(
            [FromQuery] ReportFilterRequestDto dto,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetDepartmentSummaryReportQuery(dto), cancellationToken);
            return response?.Success == true ? Ok(response) : BadRequest(response);
        }
    }
}
