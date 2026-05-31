using HealthcareHospitalManagement.Application.Commands.DoctorUnavailability;
using HealthcareHospitalManagement.Application.DTOs.DoctorUnavailability;
using HealthcareHospitalManagement.Application.Queries.DoctorUnavailability;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.DoctorUnavailability
{
    [ApiController]
    [Route("api/doctor-unavailabilities")]
    public class DoctorUnavailabilityController(IMediator mediator) : ControllerBase
    {
        // ── GET /api/doctor-unavailabilities/{id} ─────────────────────────────────
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetUnavailabilityByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctor-unavailabilities/doctor/{doctorId} ────────────────────
        [HttpGet("doctor/{doctorId:guid}")]
        public async Task<IActionResult> GetByDoctor(
            Guid doctorId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetUnavailabilitiesByDoctorQuery(doctorId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-unavailabilities/doctor/{doctorId}/date/{date} ────────
        [HttpGet("doctor/{doctorId:guid}/date/{date}")]
        public async Task<IActionResult> GetByDate(
            Guid doctorId, DateTime date,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetUnavailabilitiesByDateQuery(doctorId, date, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-unavailabilities/doctor/{doctorId}/date-range?start=&end=
        [HttpGet("doctor/{doctorId:guid}/date-range")]
        public async Task<IActionResult> GetByDateRange(
            Guid doctorId,
            [FromQuery] DateTime start, [FromQuery] DateTime end,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetUnavailabilitiesByDateRangeQuery(doctorId, start, end, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── POST /api/doctor-unavailabilities ─────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateDoctorUnavailabilityDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateDoctorUnavailabilityCommand(dto), ct);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
                : BadRequest(result);
        }

        // ── PUT /api/doctor-unavailabilities/{id} ─────────────────────────────────
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateDoctorUnavailabilityDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new UpdateDoctorUnavailabilityCommand(id, dto), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctor-unavailabilities/{id}/soft ─────────────────────────
        [HttpDelete("{id:guid}/soft")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new SoftDeleteDoctorUnavailabilityCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/doctor-unavailabilities/{id}/restore ───────────────────────
        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new RestoreDoctorUnavailabilityCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctor-unavailabilities/{id} ──────────────────────────────
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new DeleteDoctorUnavailabilityCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}
