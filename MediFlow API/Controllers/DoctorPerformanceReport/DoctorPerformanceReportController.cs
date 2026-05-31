using HealthcareHospitalManagement.Application.Commands.DoctorPerformanceReport;
using HealthcareHospitalManagement.Application.DTOs.DoctorPerformanceReport;
using HealthcareHospitalManagement.Application.Queries.DoctorPerformanceReport;
using HealthcareHospitalManagement.Domain.Enums.Report;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.DoctorPerformanceReport
{

    [ApiController]
    [Route("api/doctor-performance-reports")]
    public class DoctorPerformanceReportController(IMediator mediator) : ControllerBase
    {
        // ── GET /api/doctor-performance-reports/{id} ──────────────────────────────
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetReportByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctor-performance-reports/doctor/{doctorId} ─────────────────
        [HttpGet("doctor/{doctorId:guid}")]
        public async Task<IActionResult> GetByDoctor(
            Guid doctorId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetReportsByDoctorQuery(doctorId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-performance-reports/doctor/{doctorId}/period/{periodType}
        [HttpGet("doctor/{doctorId:guid}/period/{periodType}")]
        public async Task<IActionResult> GetByPeriod(
            Guid doctorId, ReportPeriodType periodType,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetReportsByPeriodQuery(doctorId, periodType, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-performance-reports/doctor/{doctorId}/date-range?fromDate=&toDate=
        [HttpGet("doctor/{doctorId:guid}/date-range")]
        public async Task<IActionResult> GetByDateRange(
            Guid doctorId,
            [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetReportByDateRangeQuery(doctorId, fromDate, toDate), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctor-performance-reports/department/{departmentName} ────────
        [HttpGet("department/{departmentName}")]
        public async Task<IActionResult> GetByDepartment(
            string departmentName,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetReportsByDepartmentQuery(departmentName, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-performance-reports/doctor/{doctorId}/latest ──────────
        [HttpGet("doctor/{doctorId:guid}/latest")]
        public async Task<IActionResult> GetLatest(Guid doctorId, CancellationToken ct)
        {
            var result = await mediator.Send(new GetLatestReportQuery(doctorId), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── POST /api/doctor-performance-reports ──────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateDoctorPerformanceReportDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateDoctorPerformanceReportCommand(dto), ct);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
                : BadRequest(result);
        }

        // ── PUT /api/doctor-performance-reports/{id} ──────────────────────────────
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateDoctorPerformanceReportDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new UpdateDoctorPerformanceReportCommand(id, dto), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctor-performance-reports/{id}/soft ─────────────────────
        [HttpDelete("{id:guid}/soft")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new SoftDeleteDoctorPerformanceReportCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/doctor-performance-reports/{id}/restore ───────────────────
        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new RestoreDoctorPerformanceReportCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctor-performance-reports/{id} ───────────────────────────
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new DeleteDoctorPerformanceReportCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}
