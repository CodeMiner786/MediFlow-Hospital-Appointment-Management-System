using HealthcareHospitalManagement.Application.Commands.DoctorSchedule;
using HealthcareHospitalManagement.Application.DTOs.DoctorSchedule;
using HealthcareHospitalManagement.Application.Queries.DoctorSchedule;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.DoctorSchedule
{
    [ApiController]
    [Route("api/doctor-schedules")]
    public class DoctorScheduleController(IMediator mediator) : ControllerBase
    {
        // ── GET /api/doctor-schedules/{id} ────────────────────────────────────────
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetScheduleByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctor-schedules/{id}/with-slots ─────────────────────────────
        [HttpGet("{id:guid}/with-slots")]
        public async Task<IActionResult> GetWithSlots(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetScheduleWithSlotsQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctor-schedules/doctor/{doctorId} ───────────────────────────
        [HttpGet("doctor/{doctorId:guid}")]
        public async Task<IActionResult> GetByDoctor(
            Guid doctorId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetSchedulesByDoctorQuery(doctorId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-schedules/doctor/{doctorId}/day/{day} ─────────────────
        [HttpGet("doctor/{doctorId:guid}/day/{day}")]
        public async Task<IActionResult> GetByDay(
            Guid doctorId, DayOfWeek day,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetSchedulesByDayQuery(doctorId, day, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-schedules/doctor/{doctorId}/active ────────────────────
        [HttpGet("doctor/{doctorId:guid}/active")]
        public async Task<IActionResult> GetActive(
            Guid doctorId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetActiveSchedulesQuery(doctorId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-schedules/doctor/{doctorId}/check-overlap ─────────────
        [HttpGet("doctor/{doctorId:guid}/check-overlap")]
        public async Task<IActionResult> CheckOverlap(
            Guid doctorId,
            [FromQuery] DayOfWeek day,
            [FromQuery] TimeOnly start,
            [FromQuery] TimeOnly end,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new CheckScheduleOverlapQuery(doctorId, day, start, end), ct);
            return Ok(result);
        }

        // ── POST /api/doctor-schedules ────────────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateDoctorScheduleDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateDoctorScheduleCommand(dto), ct);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
                : BadRequest(result);
        }

        // ── PUT /api/doctor-schedules/{id} ────────────────────────────────────────
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateDoctorScheduleDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new UpdateDoctorScheduleCommand(id, dto), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── DELETE /api/doctor-schedules/{id}/soft ────────────────────────────────
        [HttpDelete("{id:guid}/soft")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new SoftDeleteDoctorScheduleCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/doctor-schedules/{id}/restore ──────────────────────────────
        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new RestoreDoctorScheduleCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctor-schedules/{id} ─────────────────────────────────────
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new DeleteDoctorScheduleCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}
